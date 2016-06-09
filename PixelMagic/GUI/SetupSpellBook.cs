// Icon Backlink: http://www.aha-soft.com/

using PixelMagic.Helpers;
using System;
using System.Windows.Forms;

namespace PixelMagic.GUI
{
    public partial class SetupSpellBook : Form
    {
        public SetupSpellBook()
        {
            InitializeComponent();
        }

        private void SetupSpellBook_Load(object sender, EventArgs e)
        {
            txtAddonAuthor.Text = Environment.UserName;
            txtAddonName.Text = Environment.MachineName;
            txtAddonInterface.Text = "60200";
            
            dgSpells.DataSource = SpellBook.dtSpells;
            dgAuras.DataSource = SpellBook.dtAuras;
        }

        private void cmdAddSpell_Click(object sender, EventArgs e)
        {
            SpellBook.AddSpell(nudSpellId, txtSpellName);            
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
            SpellBook.Save();
        }
    }
}
