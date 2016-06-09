using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

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

            dtAuras = new DataTable();
            dtAuras.Columns.Add("Aura Id");
            dtAuras.Columns.Add("Aura Name");

            Load();
        }

        public static void AddSpell(NumericUpDown spellId, TextBox spellName)
        {
            AddSpell(int.Parse(spellId.Value.ToString()), spellName.Text);
        }

        public static void AddSpell(int spellId, string spellName)
        {
            if (dtSpells.Select($"[Spell Id] = {spellId}").Count() == 0)
            {
                dtSpells.Rows.Add(spellId, spellName);
                Spells.Add(new Spell(spellId, spellName));
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
            if (dtAuras.Select($"[Aura Id] = {auraId}").Count() == 0)
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
            if (dtSpells.Select($"[Spell Id] = {spellId}").Count() == 1)
            {
                dtSpells.Rows.Remove(dtSpells.Select($"[Spell Id] = {spellId}").FirstOrDefault());
                Spells.Remove(Spells.FirstOrDefault(s => s.SpellId == spellId));
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
            if (dtAuras.Select($"[Aura Id] = {auraId}").Count() == 1)
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

            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\SpellBook.db"))
            {
                string line;

                while((line = sr.ReadLine()) != null)
                {
                    string []split = line.Split(',');

                    if (split[0] == "Spell")
                    {
                        AddSpell(int.Parse(split[1]), split[2]);
                    }

                    if (split[0] == "Aura")
                    {
                        AddAura(int.Parse(split[1]), split[2]);
                    }
                }

                sr.Close();
            }
        }

        public static void Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\SpellBook.db"))
                {
                    foreach (Spell spell in Spells)
                    {
                        sw.WriteLine($"Spell,{spell.SpellId},{spell.SpellName}");
                    }
                    foreach (Aura aura in Auras)
                    {
                        sw.WriteLine($"Aura,{aura.AuraId},{aura.AuraName}");
                    }
                    sw.Close();
                }

                GenerateLUAFile();

                MessageBox.Show("Spell Book Saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void GenerateLUAFile()
        {

        }
    }
}
