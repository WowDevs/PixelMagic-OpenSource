using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace PixelMagic.GUI
{
    public partial class SelectWoWProcessToAttachTo : Form
    {
        private frmMain parent;

        public SelectWoWProcessToAttachTo(frmMain parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void refreshProcessList()
        {
            cmbWoW.Items.Clear();

            var processes = Process.GetProcessesByName("Wow");

            foreach (Process process in processes)
            {
                cmbWoW.Items.Add($"WoW x86 [Live] => {process.Id}");
            }

            processes = Process.GetProcessesByName("Wow-64");

            foreach (Process process in processes)
            {
                cmbWoW.Items.Add($"WoW x64 [Live] => {process.Id}");
            }

            processes = Process.GetProcessesByName("WowB-64");

            foreach (Process process in processes)
            {
                cmbWoW.Items.Add($"WoW x64 [Beta] => {process.Id}");
            }

            processes = Process.GetProcessesByName("WowT-64");

            foreach (Process process in processes)
            {
                cmbWoW.Items.Add($"WoW x64 [PTR] => {process.Id}");
            }

            if (cmbWoW.Items.Count > 0)
            {
                cmbWoW.SelectedIndex = 0;
                cmbWoW.Enabled = true;
            }
            else
            {
                cmbWoW.Enabled = false;
            }
        }

        private void SelectWoWProcessToAttachTo_Load(object sender, EventArgs e)
        {
            cmbWoW.KeyDown += CmbWoW_KeyDown;

            cmbWoW.Focus();

            refreshProcessList();            
        }

        private void CmbWoW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmdConnect.PerformClick();
            }
        }
        
        private void cmdConnect_Click(object sender, EventArgs e)
        {
            if (cmbWoW.Text.Contains(">"))
            {
                int PID = int.Parse(cmbWoW.Text.Split('>')[1]);
                parent.process = Process.GetProcessById(PID);
                Close();
            }
            else
            {
                MessageBox.Show("Please select a valid wow process to connect to", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            refreshProcessList();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            parent.process = null;
            Close();
        }
    }
}
