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
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace PixelMagic.Helpers
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class Ticket
    {
        private static bool Initialized;
        private static RichTextBox _rtbLogWindow;
        private static readonly Color _errorColor = Color.Red;
        private static Form _parent;
        private static bool _clearHistory;

        public static string HorizontalLine = "------------";

        private static string lastMessage;

        public static int LineCount { get; private set; }

        private static void SetDoubleBuffered(Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            if (SystemInformation.TerminalServerSession)
                return;

            var aProp = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }

        public static void Initialize(RichTextBox rtbLogWindow, Form parent, bool clearHistory = true)
        {
            _rtbLogWindow = rtbLogWindow;
            _parent = parent;
            _clearHistory = clearHistory;

            SetDoubleBuffered(rtbLogWindow);

            Initialized = true;
        }

        private static void LogActivityWithoutLineFeedOrTime(string activity, Color c, bool noSound = false)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(c, activity, true, false, noSound);                    
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
                    
                }));
        }

        public static void WriteNoTime(string activity, Color c)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(c, activity, true);
                    
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

        public static void Write(string text)
        {
            //if (text == lastMessage) // We want to avoid spamming, so we dont display duplicate messages
            //{
            //    return;
            //}

            Write(text, Color.Black);

            //lastMessage = text;
        }

        internal static void WritePixelMagic(string text, Color c)
        {
            char[] delimiterChars = {' ', ',', '.', ':', '\t'};
            var words = text.Split(delimiterChars);

            foreach (var s in words)
            {
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
            if (text == lastMessage && text == "Rotation paused until WoW Window has focus again.") // We want to avoid spamming, so we dont display duplicate messages
            {
                return;
            }

            if (_parent == null)
            {
                MessageBox.Show("Please ensure you call Log.Initialize()");
                Application.Exit();
            }

            try
            {
                _parent?.Invoke(
                    new Action(() =>
                    {
                        InternalWrite(c, text);
                    }));
            }
            catch
            {

            }
            lastMessage = text;
        }

        public static void WriteNewLine()
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(Color.Black, "", true);
                }));
        }

        public static void DrawHorizontalLine()
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(Color.LightGray, HorizontalLine, true);
                }));
        }

        public static void Write(Color color, string format, params object[] args)
        {
            _parent.Invoke(
                new Action(() =>
                {
                    InternalWrite(color, string.Format(format, args));
                }));
        }

        private static void InternalWrite(Color color, string text, bool noTime = false, bool lineFeed = true, bool noSound = false)
        {
            try
            {
                if (color == Color.Red && ConfigFile.PlayErrorSounds)
                {
                    if (!noSound)
                    {
                        SystemSounds.Hand.Play();
                    }
                }

                var rtb = _rtbLogWindow;

                rtb.SuspendLayout();

                // We remove the top 1000 lines from the textbox when we reach 2000 lines
                // We are only doing this update @ 2000 lines to prevent flickering
                // Flickering is not 100% removed but it is reduced to an acceptable level.

                if (rtb.Lines.Length > 2000 && _clearHistory)
                {
                    rtb.Select(0, rtb.GetFirstCharIndexFromLine(rtb.Lines.Length - 1000));
                    rtb.SelectedText = "";
                }

                LineCount = rtb.Lines.Length;

                rtb.SelectionStart = rtb.Text.Length;
                rtb.SelectionLength = 0;

                if (!noTime)
                {
                    rtb.SelectionColor = Color.Gray;
                    rtb.AppendText($"[{DateTime.Now.ToString("HH:mm:ss")}] ");
                }

                rtb.SelectionColor = color;
                rtb.AppendText(lineFeed ? $"{text}\r" : $"{text}");

                rtb.ClearUndo();

                rtb.ResumeLayout(false);
            }
            catch
            {
                // ignored
            }
        }
    }
}