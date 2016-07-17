using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable MemberCanBePrivate.Global

namespace PixelMagic.Helpers
{
    public static class SpellBook
    {
        private static string FullRotationFilePath = "";

        public static List<Spell> Spells;
        public static List<Aura> Auras;

        public static DataTable dtSpells;
        public static DataTable dtAuras;

        public static bool Initialize(string fullRotationFilePath)
        {
            FullRotationFilePath = fullRotationFilePath;

            Spells = new List<Spell>();
            Auras = new List<Aura>();

            dtSpells = new DataTable();            
            dtSpells.Columns.Add("Spell Id");
            dtSpells.Columns.Add("Spell Name");
            dtSpells.Columns.Add("Key Bind");
            dtSpells.Columns.Add("InternalNo"); // This stores the spell no in the array of spells that will be used on the addon

            dtAuras = new DataTable();
            dtAuras.Columns.Add("Aura Id");
            dtAuras.Columns.Add("Aura Name");
            dtAuras.Columns.Add("InternalNo"); // This stores the aura no in the array of auras that will be used on the addon

            return Load();
        }

        public static void AddSpell(NumericUpDown spellId, TextBox spellName, TextBox keyBind)
        {
            AddSpell(int.Parse(spellId.Value.ToString()), spellName.Text, keyBind.Text);
        }

        private static void RenumberSpells()
        {
            var i = 1;

            foreach(DataRow dr in dtSpells.Rows)
            {
                dr["InternalNo"] = i;
                i++;
            }
        }

        private static void RenumberAuras()
        {
            var i = 1;

            foreach (DataRow dr in dtAuras.Rows)
            {
                dr["InternalNo"] = i;
                i++;
            }
        }

        public static void AddSpell(int spellId, string spellName, string keyBind)
        {
            if (dtSpells != null && dtSpells.Select($"[Spell Id] = {spellId}").Length == 0)
            {
                dtSpells.Rows.Add(spellId, spellName, keyBind, 0);
                RenumberSpells();

                var newSpellId = int.Parse(dtSpells.Select($"[Spell Id] = {spellId}")[0]["InternalNo"].ToString());

                Spells.Add(new Spell(spellId, spellName, keyBind, newSpellId));                
            }
            else
            {
                MessageBox.Show("The current spell already exists, you may not add it twice", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void AddAura(NumericUpDown auraId, TextBox auraName)
        {
            AddAura(int.Parse(auraId.Value.ToString()), auraName.Text);
        }

        public static void AddAura(int auraId, string auraName)
        {
            if (dtAuras != null && dtAuras.Select($"[Aura Id] = {auraId}").Length == 0)
            {
                dtAuras.Rows.Add(auraId, auraName);
                RenumberAuras();

                var newAuraId = int.Parse(dtAuras.Select($"[Aura Id] = {auraId}")[0]["InternalNo"].ToString());

                Auras.Add(new Aura(auraId, auraName, newAuraId));
            }
            else
            {
                MessageBox.Show("The current aura already exists, you may not add it twice", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RemoveSpell(NumericUpDown spellId)
        {
            RemoveSpell(int.Parse(spellId.Value.ToString()));
        }

        public static void RemoveSpell(int spellId)
        {
            if (dtSpells.Select($"[Spell Id] = {spellId}").Length == 1)
            {
                dtSpells.Rows.Remove(dtSpells.Select($"[Spell Id] = {spellId}").FirstOrDefault());
                Spells.Remove(Spells.FirstOrDefault(s => s.SpellId == spellId));

                RenumberSpells();
            }
            else
            {
                MessageBox.Show("The current spell does not exist in the spellbook yet, so it can't be removed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void RemoveAura(NumericUpDown auraId)
        {
            RemoveAura(int.Parse(auraId.Value.ToString()));
        }

        public static void RemoveAura(int auraId)
        {
            if (dtAuras.Select($"[Aura Id] = {auraId}").Length == 1)
            {
                dtAuras.Rows.Remove(dtAuras.Select($"[Aura Id] = {auraId}").FirstOrDefault());
                Auras.Remove(Auras.FirstOrDefault(a => a.AuraId == auraId));
            }
            else
            {
                MessageBox.Show("The current aura does not exist in the spellbook yet, so it can't be removed", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool Load()
        {               
            using (var sr = new StreamReader(FullRotationFilePath))
            {
                string fileContents = sr.ReadToEnd();

                bool encrypted = (FullRotationFilePath.EndsWith(".enc"));

                if (encrypted)
                {
                    fileContents = Encryption.Decrypt(fileContents);
                }

                bool addonLines = false;
                bool readLines = false;
                
                foreach (string line in fileContents.Split('\n'))
                {                
                    if (line.Contains("AddonDetails.db"))
                    {
                        addonLines = true;
                    }

                    if (line.Contains("SpellBook.db"))
                    {
                        readLines = true;
                    }

                    if (addonLines)
                    {
                        if (line.Contains("AddonLines.db"))
                            continue;

                        var split = line.Split('=');

                        if (split[0] == "AddonAuthor")
                        {
                            AddonAuthor = split[1];
                        }
                        
                        if (split[0] == "WoWVersion")
                        {
                            InterfaceVersion = split[1];
                        }
                    }

                    if (readLines)
                    {
                        if (line.Contains("SpellBook.db"))
                            continue;

                        var split = line.Split(',');

                        if (split[0] == "Spell")
                        {
                            AddSpell(int.Parse(split[1]), split[2], split[3]);
                        }

                        if (split[0] == "Aura")
                        {
                            AddAura(int.Parse(split[1]), split[2]);
                        }
                    }
                }

                sr.Close();

                if (addonLines && readLines)  // If the word "AddonDetails.db" and "SpellBook.db" exists in the rotation.cs file
                {
                    RenumberSpells();
                    Log.Write($"Found {Spells.Count} spells defined", Color.Gray);
                    Log.Write($"Found {Auras.Count} auras defined", Color.Gray);
                    Log.Write("SpellBook loaded.");

                    if (!File.Exists(AddonPath + "\\" + AddonName + "\\" + AddonName + ".toc"))
                    {
                        return GenerateLUAFile();
                    }
                }
                else
                {                    
                    Log.Write("Failed to load addon details or spellbook from rotation file, please ensure that it is not missing.", Color.Red);
                    Log.Write("you can see the file: " + Application.StartupPath + "\\Rotations\\Hunter\\Hunter.cs for reference", Color.Red);

                    return false;
                }
            }

            return false;
        }

        public static string AddonAuthor;
        public static string InterfaceVersion;
        public static string AddonName => ConfigFile.ReadValue("PixelMagic", "AddonName");

        public static void Save(TextBox author, string interfaceVersion, TextBox addonName)
        {
            AddonAuthor = author.Text;
            InterfaceVersion = interfaceVersion;
            
            try
            {
                string fullRotationText = "";

                bool encrypted = (FullRotationFilePath.EndsWith(".enc"));

                using (var sr = new StreamReader(FullRotationFilePath))
                {
                    bool readLines = true;
                    string fileContents = sr.ReadToEnd();
                    
                    if (encrypted)
                    {
                        fileContents = Encryption.Decrypt(fileContents);
                    }

                    foreach (string line in fileContents.Split('\n'))
                    {
                        if (line.Contains("AddonDetails.db"))
                        {
                            readLines = false;
                        }

                        if (readLines)
                        {
                            if (line.StartsWith("/*"))
                            {
                                fullRotationText += line;
                            }
                            else
                            {
                                fullRotationText += line + Environment.NewLine;
                            }
                        }
                    }

                    sr.Close();
                }

                string updatedRotationText = fullRotationText + Environment.NewLine;
                updatedRotationText += "[AddonDetails.db]" + Environment.NewLine;
                updatedRotationText += $"AddonAuthor={AddonAuthor}" + Environment.NewLine;
                updatedRotationText += $"AddonName={AddonName}" + Environment.NewLine;
                updatedRotationText += $"WoWVersion={InterfaceVersion}" + Environment.NewLine;

                updatedRotationText += "[SpellBook.db]" + Environment.NewLine;

                foreach (var spell in Spells)
                {
                    updatedRotationText += $"Spell,{spell.SpellId},{spell.SpellName},{spell.KeyBind}" + Environment.NewLine;
                }
                foreach (var aura in Auras)
                {
                    updatedRotationText += $"Aura,{aura.AuraId},{aura.AuraName}" + Environment.NewLine;
                }

                updatedRotationText += "*/";

                using (var sw = new StreamWriter(FullRotationFilePath, false))
                {
                    if (encrypted)
                    {
                        updatedRotationText = Encryption.Encrypt(updatedRotationText);
                        sw.WriteLine(updatedRotationText);
                    }
                    else
                    {
                        sw.WriteLine(updatedRotationText);
                    }                    

                    sw.Close();
                }

                GenerateLUAFile();

                MessageBox.Show("Spell Book Saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string AddonPath
        {
            get
            {
                return $"{WoW.AddonPath}\\{AddonName}";
            }
        }

        public static bool GenerateLUAFile()
        {
            try
            {
                if (!Directory.Exists(AddonPath))
                    Directory.CreateDirectory(AddonPath);

                Log.Write($"Creating Addon from SpellBook, AddonName will be [{AddonName}]...");

                Log.Write($"Creating file: [{AddonName}.toc]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\{AddonName}.toc"))
                {
                    //  ## Author: WiNiFiX
                    //  ## Interface: 60200
                    //  ## Title: DoIt
                    //  ## Version: 1.0.0
                    //  ## SavedVariablesPerCharacter: DoItOptions
                    //  DoItBase.lua

                    sr.WriteLine($"## Author: {AddonAuthor}");
                    sr.WriteLine($"## Interface: {InterfaceVersion}");
                    sr.WriteLine($"## Title: {AddonName}");
                    sr.WriteLine($"## Version: {Application.ProductVersion}");
                    sr.WriteLine($"## SavedVariablesPerCharacter: {AddonName}_settings");
                    sr.WriteLine($"{AddonName}.lua");
                    sr.Close();
                }

                Log.Write($"Creating file: [{AddonName}.lua]", Color.Gray);

                using (var sr = new StreamWriter($"{AddonPath}\\{AddonName}.lua"))
                {
                    //local cooldowns = { --These should be spellIDs for the spell you want to track for cooldowns
                    //    56641,    -- Steadyshot
                    //    3044,     -- Arcane Shot
                    //    34026     -- Kill Command
                    //}

                    var cooldowns = "local cooldowns = { --These should be spellIDs for the spell you want to track for cooldowns" + Environment.NewLine;

                    foreach (var spell in Spells)
                    {
                        if (spell.InternalSpellNo == Spells.Count)  // We are adding the last spell, dont include the comma
                        {
                            cooldowns += $"    {spell.SpellId} \t -- {spell.SpellName}" + Environment.NewLine;
                        }
                        else
                        {
                            cooldowns += $"    {spell.SpellId},\t -- {spell.SpellName}" + Environment.NewLine;
                        }
                    }

                    cooldowns += "}" + Environment.NewLine;

                    sr.Write(cooldowns);

                    var auras = "local auras = { --These should be auraIDs for the spell you want to track " + Environment.NewLine;

                    foreach (var aura in Auras)
                    {
                        if (aura.InternalAuraNo == Auras.Count)  // We are adding the last aura, dont include the comma
                        {
                            auras += $"    {aura.AuraId} \t -- {aura.AuraName}" + Environment.NewLine;
                        }
                        else
                        {
                            auras += $"    {aura.AuraId},\t -- {aura.AuraName}" + Environment.NewLine;
                        }
                    }

                    auras += "}" + Environment.NewLine;

                    sr.Write(auras);

                    var luaContents = Addon.LuaContents;

                    luaContents = luaContents.Replace("DoIt", AddonName);

                    string AddonInterfaceVersion = InterfaceVersion;
                    if (InterfaceVersion.Contains("-"))
                        AddonInterfaceVersion = InterfaceVersion.Split('-')[1].Trim();

                    if (AddonInterfaceVersion == "70000") // Legion changes as per http://www.wowinterface.com/forums/showthread.php?t=53248
                    {
                        luaContents = luaContents.Replace("SetTexture", "SetColorTexture");

                        // For now the below are disabled till further testing is done
                        // luaContents = luaContents.Replace(@"UnitPower(""player"");", @"UnitPower(""player"", UnitPowerType(""player""))");
                        // luaContents = luaContents.Replace(@"UnitPowerMax(""player"");", @"UnitPowerMax(""player"", UnitPowerType(""player""))");
                    }

                    sr.WriteLine(luaContents);
                    sr.Close();
                }

                Log.Write("Addon file generated.", Color.Green);
                Log.Write($"Make sure that the addon: [{AddonName}] is enabled in your list of WoW Addons or the rotation bot will fail to work", Color.Black);

                WoW.SendMacro("/console scriptErrors 1");   // Show wow Lua errors
                WoW.SendMacro("/reload");

                return true;
            }
            catch(Exception ex)
            {
                Log.Write("Failed to generate addon file:", Color.Red);
                Log.Write(ex.Message, Color.Red);

                return false;
            }
        }
    }
}