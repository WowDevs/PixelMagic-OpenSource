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
                w.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

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