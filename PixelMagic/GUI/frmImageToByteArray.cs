using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelMagic.GUI.GUI
{
    public partial class frmImageToByteArray : Form
    {
        public frmImageToByteArray()
        {
            InitializeComponent();
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();

            if (res == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;

                picSample.Image = Image.FromFile(txtPath.Text);

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                
                var bytes = imageToByteArray(picSample.Image);
                string byteString = string.Join(",", bytes);
                txtByteString.Text = byteString;

                Cursor = Cursors.Arrow;
                txtByteString.SelectAll();
            }
        }

        private void picSample_Click(object sender, EventArgs e)
        {

        }
    }
}
