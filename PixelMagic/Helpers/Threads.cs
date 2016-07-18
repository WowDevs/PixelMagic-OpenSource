//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System.Windows.Forms;

namespace PixelMagic.Helpers
{
    public static class Threads
    {
        public static void UpdateTextBox(TextBox txt, string text)
        {
            if (text == null)
                return;

            try
            {
                if (txt.InvokeRequired)
                {
                    UpdateTextBoxCallback d = UpdateTextBox;
                    txt.Invoke(d, txt, text);
                    return;
                }

                txt.Text = text;
            }
            catch
            {
                // This catch is simply here to avoid the OCCASIONAL crash of the application when closing it by pressing the stop button in visual studio while it is running tasks
            }
        }

        private delegate void UpdateTextBoxCallback(TextBox txt, string text);

        public static void UpdateProgressBar(ProgressBar prg, int value)
        {
            try
            {
                if (prg.InvokeRequired)
                {
                    UpdateProgressBarCallback d = UpdateProgressBar;
                    prg.Invoke(d, prg, value);
                    return;
                }

                prg.Value = value;
            }
            catch
            {
                // This catch is simply here to avoid the OCCASIONAL crash of the application when closing it by pressing the stop button in visual studio while it is running tasks
            }
        }

        private delegate void UpdateProgressBarCallback(ProgressBar prg, int value);

        public static void UpdateProgressBar2(ColorProgressBar.ColorProgressBar prg, int value)
        {
            try
            {
                if (prg.InvokeRequired)
                {
                    UpdateProgressBarCallback2 d = UpdateProgressBar2;
                    prg.Invoke(d, prg, value);
                    return;
                }

                prg.Value = value;
            }
            catch
            {
                // This catch is simply here to avoid the OCCASIONAL crash of the application when closing it by pressing the stop button in visual studio while it is running tasks
            }
        }

        private delegate void UpdateProgressBarCallback2(ColorProgressBar.ColorProgressBar prg, int value);
    }
}