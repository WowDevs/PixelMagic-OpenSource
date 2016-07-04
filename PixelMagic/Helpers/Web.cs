//////////////////////////////////////////////////
//                                              //
//   See License.txt for Licensing information  //
//                                              //
//////////////////////////////////////////////////

using System;
using System.Net;

namespace PixelMagic.Helpers
{
    public static class Web
    {
        public static string GetString(string url)
        {
            using (var w = new WebClient())
            {
                var stringData = string.Empty;
                try
                {
                    stringData = w.DownloadString(url);
                }
                catch (Exception)
                {
                    // ignored
                }

                return stringData;
            }
        }
    }
}