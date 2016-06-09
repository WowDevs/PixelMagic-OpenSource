//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PixelMagic.Helpers
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class Log
    {
        private static bool Initialized = false;
        private static StreamWriter _sw;
        private static RichTextBox _rtbLogWindow;
        private readonly static Color _errorColor = Color.Red;
        private static Form _parent;
        private static bool _clearHistory;

        private static int _lineCount;

        public static int LineCount
        {
            get
            {
                return _lineCount;
            }
        }

        public static string HorizontalLine = "------------";

        private static void SetDoubleBuffered(Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (SystemInformation.TerminalServerSession)
                return;

            PropertyInfo aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        public static void Initialize(RichTextBox rtbLogWindow, Form parent, bool clearHistory = true)
        {
            if (!Directory.Exists(Application.StartupPath + "\\Logs\\" + DateTime.Now.ToString("yyyy-MMM")))
                Directory.CreateDirectory(Application.StartupPath + "\\Logs\\" + DateTime.Now.ToString("yyyy-MMM"));

            _sw = new StreamWriter(Application.StartupPath + "\\Logs\\" + DateTime.Now.ToString("yyyy-MMM") + "\\" + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".txt")
            {
                AutoFlush = true
            };

            _rtbLogWindow = rtbLogWindow;
            _parent = parent;
            _clearHistory = clearHistory;

            SetDoubleBuffered(rtbLogWindow);

            Initialized = true;
        }

        private static void LogActivityWithoutLineFeed(string activity, Color c)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(c, activity, false, false);
                    WriteDirectlyToLogFile(activity);
                }));
        }

        private static void LogActivityWithoutLineFeedOrTime(string activity, Color c, bool noSound = false)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(c, activity, true, false, noSound);
                    WriteDirectlyToLogFile(activity);
                }));
        }

        public static void Clear()
        {
            _rtbLogWindow.Clear();
        }

        public static void WriteNoTime(string activity)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(Color.Black, activity, true);
                    WriteDirectlyToLogFile(activity);
                }));
        }

        public static void WriteNoTime(string activity, Color c)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(c, activity, true);
                    WriteDirectlyToLogFile(activity);
                }));
        }

        //public static void NewLine()
        //{
        //    LogActivity(" ", Color.Black);
        //}

        private static void LogActivity(string activity, Color c)
        {
            if (!Initialized)
                return;

            try
            {
                if (activity == string.Empty)
                {
                    DrawHorizontalLine();
                }
                else if (activity.Trim() == string.Empty)
                {
                    WriteNewLine();
                }
                else
                {
                    Write(activity, c);
                }

                Application.DoEvents();
            }
            catch (Exception execp)
            {
                LogActivity("Exception in LogActivity function\r\nError: " + execp.Message, _errorColor);
            }
        }

        public static void LogActivity(string activity)
        {
            if (!Initialized)
                return;

            try
            {
                if (activity == string.Empty)
                {
                    DrawHorizontalLine();
                }
                else if (activity.Trim() == string.Empty)
                {
                    WriteNewLine();
                }
                else
                {
                    Write(activity, Color.Black);
                }

                Application.DoEvents();
            }
            catch (Exception execp)
            {
                LogActivity("Exception in LogActivity function\r\nError: " + execp.Message, _errorColor);
            }
        }

        public static void WriteDirectlyToLogFile(string format, params object[] args)
        {
            try
            {
                if (_sw != null)
                {
                    _sw.WriteLine("[" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "] " + format, args);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "Index (zero based) must be greater than or equal to zero and less than the size of the argument list." ||
                    ex.Message == "Input string was not in a correct format.")
                {
                    try
                    {
                        _sw?.WriteLine("[" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "] " + format);
                    }
                    catch
                    {
                        LogActivity("Failed to write to log file [2] - " + ex.Message, Color.Red);
                    }
                }
                else
                {
                    LogActivity("Failed to write to log file [1] - " + ex.Message, Color.Red);
                }
            }
        }

        public static void Write(string text)
        {
            if (text == lastMessage) // We want to avoid spamming, so we dont display duplicate messages
            {
                return;
            }

            Write(text, Color.Black);

            lastMessage = text;
        }

        internal static void WritePixelMagic(string text, Color c)
        {
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            string[] words = text.Split(delimiterChars);

            for (int i = 0; i < words.Length; i++)
            {
                string s = words[i];
            
                if (s == "PixelMagic")
                {
                    LogActivityWithoutLineFeedOrTime("P", Color.Red, true);
                    LogActivityWithoutLineFeedOrTime("i", Color.Green, true);
                    LogActivityWithoutLineFeedOrTime("x", Color.Blue, true);
                    LogActivityWithoutLineFeedOrTime("e", Color.Indigo, true);
                    LogActivityWithoutLineFeedOrTime("l", Color.Red, true);
                    LogActivityWithoutLineFeedOrTime("M", Color.Green, true);
                    LogActivityWithoutLineFeedOrTime("a", Color.Blue, true);
                    LogActivityWithoutLineFeedOrTime("g", Color.Indigo, true);
                    LogActivityWithoutLineFeedOrTime("i", Color.Red, true);
                    LogActivityWithoutLineFeedOrTime("c ", Color.Green, true);
                }
                else
                {
                    LogActivityWithoutLineFeedOrTime(s + " ", c);
                }
            }

            LogActivityWithoutLineFeedOrTime(Environment.NewLine, Color.Black);
        }

        public static void Write(string text, Color c)
        {
            if (text == lastMessage) // We want to avoid spamming, so we dont display duplicate messages
            {
                return;
            }

            if (_parent == null)
            {
                MessageBox.Show("Please ensure you call Log.Initialize()");
                Application.Exit();
            }

            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(c, text);
                    WriteDirectlyToLogFile(text);
                }));

            lastMessage = text;
        }

        public static void WriteNewLine()
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(Color.Black, "", true);
                    WriteDirectlyToLogFile("");
                }));
        }

        public static void DrawHorizontalLine()
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(Color.LightGray, HorizontalLine, true);
                    WriteDirectlyToLogFile(HorizontalLine);
                }));
        }

        public static void Write(Color color, string format, params object[] args)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(color, string.Format(format, args));
                    WriteDirectlyToLogFile(format, args);
                }));
        }

        private static string lastMessage;

        [DllImport("user32.dll")]
        private static extern bool LockWindowUpdate(IntPtr hWndLock);

        private static void InternalWrite(Color color, string text, bool noTime = false, bool lineFeed = true, bool noSound = false)
        {
            try
            {
                if (color == Color.Red && ConfigFile.PlayErrorSounds)
                {
                    if (!noSound)
                    {
                        System.Media.SystemSounds.Hand.Play();
                    }
                }

                RichTextBox rtb = _rtbLogWindow;

                rtb.SuspendLayout();

                // We remove the top 1000 lines from the textbox when we reach 2000 lines
                // We are only doing this update @ 2000 lines to prevent flickering
                // Flickering is not 100% removed but it is reduced to an acceptable level.

                if (rtb.Lines.Length > 2000 && _clearHistory)
                {
                    rtb.Select(0, rtb.GetFirstCharIndexFromLine(rtb.Lines.Length - 1000));
                    rtb.SelectedText = "";
                }

                _lineCount = rtb.Lines.Length;

                rtb.SelectionStart = rtb.Text.Length;
                rtb.SelectionLength = 0;
                
                if (!noTime)
                {
                    rtb.SelectionColor = Color.Gray;
                    rtb.AppendText(string.Format("[{0}] ", DateTime.Now.ToString("HH:mm:ss")));
                }

                rtb.SelectionColor = color;
                if (lineFeed)
                {
                    rtb.AppendText(string.Format("{0}\r", text));
                }
                else
                {
                    rtb.AppendText(string.Format("{0}", text));
                }

                rtb.ClearUndo();

                rtb.ResumeLayout(false);

                ScrollToBottom(rtb);
            }
            catch
            {
                // ignored
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
        private const int WM_VSCROLL = 277;
        private const int SB_PAGEBOTTOM = 7;

        public static void ScrollToBottom(RichTextBox MyRichTextBox)
        {
            SendMessage(MyRichTextBox.Handle, WM_VSCROLL, (IntPtr)SB_PAGEBOTTOM, IntPtr.Zero);
        }
    }
}
