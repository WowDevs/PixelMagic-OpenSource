//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PixelMagic.Helpers
{
    public static class ConfigFile
    {
        private static string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static void Initialize()
        {
            path = Application.StartupPath + "\\config.ini";
        }

        public static void WriteValue(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, path);
        }

        public static string ReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);
            return temp.ToString().Trim();
        }

        public static T ReadValue<T>(string section, string key)
        {
            StringBuilder temp = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", temp, 255, path);

            if ((temp.ToString() == "") && (typeof(T) == typeof(int)))
            {
                temp.Append("0");
            }
            if ((temp.ToString() == "") && (typeof(T) == typeof(short)))
            {
                temp.Append("0");
            }
            return (T)Convert.ChangeType(temp.ToString(), typeof(T));
        }
        
        public static string LastRotation
        {
            get
            {
                return ReadValue("PixelMagic", "LastProfile").Trim();
            }
        }
        
        public static bool PlayErrorSounds
        {
            get
            {
                string playErrorsounds = ReadValue("PixelMagic", "PlayErrorSounds").Trim();

                if (playErrorsounds != "")
                {
                    return Convert.ToBoolean(playErrorsounds);
                }

                return true;
            }
            set
            {
                WriteValue("PixelMagic", "PlayErrorSounds", value.ToString());
            }
        }

        public static bool DisableOverlay
        {
            get
            {
                string disableOverlay = ReadValue("PixelMagic", "DisableOverlay").Trim();

                if (disableOverlay != "")
                {
                    return Convert.ToBoolean(disableOverlay);
                }

                return true;
            }
            set
            {
                WriteValue("PixelMagic", "DisableOverlay", value.ToString());
            }
        }

        public static decimal Pulse
        {
            get
            {
                string pulse = ReadValue("PixelMagic", "Pulse").Trim();

                if (pulse != "")
                {
                    return Convert.ToDecimal(pulse);
                }

                return 100;
            }
            set
            {
                WriteValue("PixelMagic", "Pulse", value.ToString());
            }
        }
    }
}
