using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PixelMagic;
using PixelMagic.GUI;
using PixelMagic.Helpers;

namespace PixelMagic.GUI
{
    public partial class frmSubmitTicket : Form
    {
        public frmSubmitTicket()
        {
            InitializeComponent();
        }

        private void cmdClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSpec.Text = "";
            cmbSpec.Items.Clear();

            if (cmbClass.Text == "Warrior")
            {
                cmbSpec.Items.Add("Arms");
                cmbSpec.Items.Add("Protection");
                cmbSpec.Items.Add("Fury");
            }
            if (cmbClass.Text == "Paladin")
            {
                cmbSpec.Items.Add("Holy");
                cmbSpec.Items.Add("Protection");
                cmbSpec.Items.Add("Retribution");
            }
            if (cmbClass.Text == "Hunter")
            {
                cmbSpec.Items.Add("Beast Mastery");
                cmbSpec.Items.Add("Survival");
                cmbSpec.Items.Add("Marksmanship");
            }
            if (cmbClass.Text == "Rogue")
            {
                cmbSpec.Items.Add("Subtlety");
                cmbSpec.Items.Add("Combat");
                cmbSpec.Items.Add("Assassination");
                cmbSpec.Items.Add("Outlaw");
            }
            if (cmbClass.Text == "Priest")
            {
                cmbSpec.Items.Add("Discipline");
                cmbSpec.Items.Add("Holy");
                cmbSpec.Items.Add("Shadow");
            }
            if (cmbClass.Text == "Death Knight")
            {
                cmbSpec.Items.Add("Frost");
                cmbSpec.Items.Add("Unholy");
                cmbSpec.Items.Add("Blood");
            }
            if (cmbClass.Text == "Shaman")
            {
                cmbSpec.Items.Add("Elemental");
                cmbSpec.Items.Add("Enhancement");
                cmbSpec.Items.Add("Restoration");
            }
            if (cmbClass.Text == "Mage")
            {
                cmbSpec.Items.Add("Frost");
                cmbSpec.Items.Add("Fire");
                cmbSpec.Items.Add("Arcane");
            }
            if (cmbClass.Text == "Warlock")
            {
                cmbSpec.Items.Add("Demonology");
                cmbSpec.Items.Add("Affliction");
                cmbSpec.Items.Add("Destruction");
            }
            if (cmbClass.Text == "Monk")
            {
                cmbSpec.Items.Add("Brewmaster");
                cmbSpec.Items.Add("Wind Walker");
                cmbSpec.Items.Add("Mistweaver");
            }
            if (cmbClass.Text == "Druid")
            {
                cmbSpec.Items.Add("Restoration");
                cmbSpec.Items.Add("Guardian");
                cmbSpec.Items.Add("Feral");
                cmbSpec.Items.Add("Balance");
            }
            if (cmbClass.Text == "Demon Hunter")
            {
                cmbSpec.Items.Add("Havoc");
                cmbSpec.Items.Add("Vengeance");
            }            
        }

        private void frmSubmitTicket_Load(object sender, EventArgs e)
        {
            txtRotationPath.Text = frmMain.combatRoutine.FileName;
        }

        private void cmdSubmitTicket_Click(object sender, EventArgs e)
        {
            if (cmbClass.Text == "")
            {
                MessageBox.Show("Please select a valid class", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (cmbSpec.Text == "")
            {
                MessageBox.Show("Please select a valid spec", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtRotationPath.Text == "")
            {
                MessageBox.Show("Please select a valid combat rotation path", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (txtComments.Text == "")
            {
                MessageBox.Show("Enter in a brief description of what you were doing when it crashed (what was the last thing you clicked)", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string ticketContents = "";
            ticketContents += "Class\t:"  + cmbClass.Text + Environment.NewLine;
            ticketContents += "Spec \t:"  + cmbSpec.Text + Environment.NewLine;
            ticketContents += "Notes\t: " + txtComments.Text + Environment.NewLine;
            ticketContents += "File \t: " + txtRotationPath.Text + Environment.NewLine;
            
            frmTicket f = new frmTicket(ticketContents);
            f.ShowDialog();

            Close();
        }
    }
}
