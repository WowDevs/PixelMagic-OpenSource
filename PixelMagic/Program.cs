//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Windows.Forms;

namespace PixelMagic.GUI
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}