using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;

namespace MyStream.Utilities
{
    public class XmlHelper
    {
        private static Regex _StripTagEx = new Regex("</?[^>]+>", RegexOptions.Compiled);

        public static string StripTags(string html, int trimAt)
        {
            string plainText = _StripTagEx.Replace(html, string.Empty);
            return plainText.Substring(0, Math.Min(plainText.Length, trimAt));
        }
    }
}
