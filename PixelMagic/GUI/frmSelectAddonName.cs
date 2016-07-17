using System;
using System.Diagnostics;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace PixelMagic.GUI.GUI
{
    public partial class frmSelectAddonName : Form
    {
        public frmSelectAddonName()
        {
            InitializeComponent();
        }
        
        private void SelectWoWProcessToAttachTo_Load(object sender, EventArgs e)
        {
        
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            if (txtAddonName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter in a valid wow addon name", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ConfigFile.WriteValue("PixelMagic", "AddonName", txtAddonName.Text);
            Close();
        }
                
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
