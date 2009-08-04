// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.IO;

namespace MyStream.Utilities
{
    public class WebHelper
    {
        public static WebResponse GetResponse(string url)
        {
            return HttpWebRequest.Create(url).GetResponse();
        }

        public static string DownloadString(string url)
        {
            return GetStringResponse(GetResponse(url));
        }

        public static string GetStringResponse(WebResponse response)
        {
            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }
    }
}
