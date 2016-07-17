using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PixelMagic.Helpers;
using System.Diagnostics;

namespace PixelMagic.GUI
{
    public partial class frmTicket : Form
    {
        private string text;

        public frmTicket(string text)
        {
            this.text = text;
            InitializeComponent();
        }
        
        private void frmTicket_Load(object sender, EventArgs e)
        {
            Ticket.Initialize(richTextBox1, this);

            foreach(string line in text.Split('\n'))
            {
                Ticket.WriteNoTime(line.Replace("\r", "").Replace("\n", ""));                
            }
                        
            Ticket.WriteNoTime("[B]Rotation File Contents[/B]");
            Ticket.WriteNoTime(SpellBook.RotationFileContents, Color.Gray);
            Ticket.WriteNoTime(" ");
            Ticket.WriteNoTime("[B]Log File Contents[/B]");
            Ticket.WriteNoTime(frmMain.rtbLog.Text);
        }

        private void cmdOpenWebsite_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.ownedcore.com/forums/world-of-warcraft/world-of-warcraft-bots-programs/wow-bots-questions-requests/542750-pixel-based-bot.html");
        }
    }
}

