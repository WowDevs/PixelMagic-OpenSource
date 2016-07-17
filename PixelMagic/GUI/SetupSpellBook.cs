// Icon Backlink: http://www.aha-soft.com/

using System;
using System.Windows.Forms;
using PixelMagic.Helpers;
using System.Globalization;
using System.Linq;
using System.Data;

// ReSharper disable once CheckNamespace

namespace PixelMagic.GUI
{
    public partial class SetupSpellBook : Form
    {
        public SetupSpellBook()
        {
            InitializeComponent();
        }

        private string AddonInterfaceVersion => cmbWowVersion.Text.Split('-')[1].Trim();

        private void SetupSpellBook_Load(object sender, EventArgs e)
        {
            var ti = new CultureInfo("en-ZA", false).TextInfo;
            txtAddonAuthor.Text = ti.ToTitleCase(Environment.UserName);

            var machineName = new string(Environment.MachineName.Where(char.IsLetter).ToArray()).ToLower();
            txtAddonName.Text = ti.ToTitleCase(machineName);

            txtAddonAuthor.Text = SpellBook.AddonAuthor;
            txtAddonName.Text = ConfigFile.ReadValue("PixelMagic", "AddonName");

            try
            {
                var intVer = SpellBook.InterfaceVersion.Replace("\n", "").Replace("\r", "");

                foreach (var item in cmbWowVersion.Items)
                {
                    if (item.ToString().Contains(intVer)) cmbWowVersion.SelectedItem = item;
                }
            }
            catch(Exception ex)
            {

            }
            
            dgSpells.DataSource = SpellBook.dtSpells;
            dgAuras.DataSource = SpellBook.dtAuras;
        }

        private void cmdAddSpell_Click(object sender, EventArgs e)
        {
            SpellBook.AddSpell(nudSpellId, txtSpellName, txtKeyBind);
        }

        private void cmdRemoveSpell_Click(object sender, EventArgs e)
        {
            SpellBook.RemoveSpell(nudSpellId);
        }

        private void cmdAddAura_Click(object sender, EventArgs e)
        {
            SpellBook.AddAura(nudAuraId, txtAuraName);
        }

        private void cmdRemoveAura_Click(object sender, EventArgs e)
        {
            SpellBook.RemoveAura(nudAuraId);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cmbWowVersion.Text == "")
            {
                MessageBox.Show("Please select a valid wow version", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SpellBook.Save(txtAddonAuthor, AddonInterfaceVersion, txtAddonName);
        }
    }
}