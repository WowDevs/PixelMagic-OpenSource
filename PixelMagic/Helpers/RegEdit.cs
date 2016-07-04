//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using Microsoft.Win32;

namespace PixelMagic.Helpers
{
    public static class RegEdit
    {
        public static string HKLMReadKey(string keyName, string valueName)
        {
            return (string)Registry.LocalMachine.OpenSubKey(keyName)?.GetValue(valueName);
        }
    }
}
