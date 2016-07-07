using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable MemberCanBePrivate.Global

namespace PixelMagic.Helpers
{
    public static class SpellBook
    {
        public static List<Spell> Spells;
        public static List<Aura> Auras;

        public static DataTable dtSpells;
        public static DataTable dtAuras;

        public static void Initialize()
        {
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

            Load();
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
                Auras.Add(new Aura(auraId, auraName));
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

        public static void Load()
        {
            if (!File.Exists(Application.StartupPath + "\\SpellBook.db"))
                return;

            using (var sr = new StreamReader(Application.StartupPath + "\\SpellBook.db"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
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

                sr.Close();

                RenumberSpells();
            }
        }

        private static string AddonAuthor;
        private static string InterfaceVersion;
        private static string AddonName;

        public static void Save(TextBox author, string interfaceVersion, TextBox addonName)
        {
            AddonAuthor = author.Text;
            InterfaceVersion = interfaceVersion;
            AddonName = addonName.Text;

            try
            {
                using (var sw = new StreamWriter(Application.StartupPath + "\\SpellBook.db"))
                {
                    foreach (var spell in Spells)
                    {
                        sw.WriteLine($"Spell,{spell.SpellId},{spell.SpellName},{spell.KeyBind}");
                    }
                    foreach (var aura in Auras)
                    {
                        sw.WriteLine($"Aura,{aura.AuraId},{aura.AuraName}");
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

        public static void GenerateLUAFile()
        {
            if (!Directory.Exists(AddonPath))
                Directory.CreateDirectory(AddonPath);

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

                var luaContents = Addon.LuaContents;

                luaContents = luaContents.Replace("DoIt", AddonName);

                if (InterfaceVersion == "70000") // Legion
                {
                    luaContents = luaContents.Replace("SetTexture", "SetColorTexture");
                }

                sr.WriteLine(luaContents);
                sr.Close();
            }
        }
    }
}