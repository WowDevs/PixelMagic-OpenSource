//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System.Windows.Forms;

namespace PixelMagic.Helpers
{
    public class Threads
    {
        delegate void UpdateTextBoxCallback(TextBox txt, string text);

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

        delegate void UpdateWebBrowserCallback(WebBrowser web, string text);

        public static void UpdateWebBrowserText(WebBrowser web, string text)
        {
            if (web.InvokeRequired)
            {
                UpdateWebBrowserCallback d = UpdateWebBrowserText;
                web.Invoke(d, web, text);
                return;
            }

            web.DocumentText = text;
        }
    }
}
