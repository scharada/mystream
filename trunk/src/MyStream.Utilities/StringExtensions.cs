// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Web;
using System.Globalization;
using MyStream.Utilities;
using System.Xml.Linq;

namespace MyStream.Utilities
{
    public static class StringExtension
    {
        private static readonly Regex WebUrlExpression = new Regex(@"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex EmailExpression = new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$", RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex StripHTMLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

        private static readonly char[] IllegalUrlCharacters = new[] { ';', '/', '\\', '?', ':', '@', '&', '=', '+', '$', ',', '<', '>', '#', '%', '.', '!', '*', '\'', '"', '(', ')', '[', ']', '{', '}', '|', '^', '`', '~', '–', '‘', '’', '“', '”', '»', '«' };

        public static string ToTweet(this string s, string anchorCssClass = "")
        { 
            var status = HttpUtility.HtmlEncode(s);

            status = Regex.Replace(status, "[A-Za-z]+://[A-Za-z0-9-_]+.[A-Za-z0-9-_:%&?/.=]+", delegate(Match m)
            {
                return string.Format("<a class=\"{1}\" href=\"{0}\">{0}</a>", m.Value, anchorCssClass);
            }, RegexOptions.Compiled);

            status = Regex.Replace(status, "[@]+[A-Za-z0-9-_]+", delegate(Match m)
            {
                var user = m.Value.Replace("@", "");
                return string.Format("@<a class=\"{1}\" href=\"http://twitter.com/{0}\">{0}</a>", user, anchorCssClass);
            }, RegexOptions.Compiled);

            status = Regex.Replace(status, "[#]+[A-Za-z0-9-_]+", delegate(Match m)
            {
                var tag = m.Value.Replace("#", "");
                return string.Format("<a class=\"{1}\" href=\"http://search.twitter.com/search?q=%23{0}\">#{0}</a>", tag, anchorCssClass);
            }, RegexOptions.Compiled);

            var name = status.Substring(0, status.IndexOf(": "));
            return string.Format("<a class=\"{1}\" href=\"http://twitter.com/{0}\">{0}</a>{2}", name, anchorCssClass, status.Substring(name.Length));
        }

        public static string TrimNull(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            else
                return s.Trim();
        }

        public static string TrimValue(this XElement e)
        {
            if (null == e)
                return string.Empty;
            else
                return e.Value.TrimNull();
        }

        public static bool IsWebUrl(this string target)
        {
            return !string.IsNullOrEmpty(target) && WebUrlExpression.IsMatch(target);
        }

        public static bool IsEmail(this string target)
        {
            return !string.IsNullOrEmpty(target) && EmailExpression.IsMatch(target);
        }

        public static string NullSafe(this string target)
        {
            return (target ?? string.Empty).Trim();
        }

        public static string FormatWith(this string target, params object[] args)
        {
            ArgValidator.IsNotEmpty(target, "target");

            return string.Format(CultureInfo.CurrentCulture, target, args);
        }

        public static string Hash(this string target)
        {
            ArgValidator.IsNotEmpty(target, "target");
            target = target.ToUpperInvariant();

            using (MD5 md5 = MD5.Create())
            {
                byte[] data = Encoding.Unicode.GetBytes(target);
                byte[] hash = md5.ComputeHash(data);

                return Convert.ToBase64String(hash);
            }
        }

        public static string WrapAt(this string target, int index)
        {
            const int DotCount = 3;

            ArgValidator.IsNotEmpty(target, "target");
            ArgValidator.IsNotNegativeOrZero(index, "index");

            return (target.Length < index) ? target : string.Concat(target.Substring(0, index), new string('.', DotCount));
        }

        public static string StripHtml(this string target)
        {
            return StripHTMLExpression.Replace(target, string.Empty);
        }

        public static Guid ToGuid(this string target)
        {
            Guid result = Guid.Empty;

            if ((!string.IsNullOrEmpty(target)) && (target.Trim().Length == 22))
            {
                string encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");

                try
                {
                    byte[] base64 = Convert.FromBase64String(encoded);

                    result = new Guid(base64);
                }
                catch (FormatException)
                {
                }
            }

            return result;
        }

        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            T convertedValue = defaultValue;

            if (!string.IsNullOrEmpty(target))
            {
                try
                {
                    convertedValue = (T)Enum.Parse(typeof(T), target.Trim(), true);
                }
                catch (ArgumentException)
                {
                }
            }

            return convertedValue;
        }

        public static string ToLegalUrl(this string target)
        {
            if (string.IsNullOrEmpty(target))
            {
                return target;
            }

            target = target.Trim();

            if (target.IndexOfAny(IllegalUrlCharacters) > -1)
            {
                foreach (char character in IllegalUrlCharacters)
                {
                    target = target.Replace(character.ToString(CultureInfo.CurrentCulture), string.Empty);
                }
            }

            target = target.Replace(" ", "-");

            while (target.Contains("--"))
            {
                target = target.Replace("--", "-");
            }

            return target;
        }

        public static string ToSlug(this string source)
        {
            var regex = new Regex(@"([^a-z0-9\-]?)");
            var slug = string.Empty;

            if (!string.IsNullOrEmpty(source))
            {
                slug = source.Trim().ToLower();
                slug = slug.Replace(' ', '-');
                slug = slug.Replace("---", "-");
                slug = slug.Replace("--", "-");
                if (regex != null)
                    slug = regex.Replace(slug, "");

                if (slug.Length * 2 < source.Length)
                    return "";

                if (slug.Length > 100)
                    slug = slug.Substring(0, 100);
            }

            return slug;
        }

        public static string UrlEncode(this string target)
        {
            return HttpUtility.UrlEncode(target);
        }

        public static string UrlDecode(this string target)
        {
            return HttpUtility.UrlDecode(target);
        }

        public static string AttributeEncode(this string target)
        {
            return HttpUtility.HtmlAttributeEncode(target);
        }

        public static string HtmlEncode(this string target)
        {
            return HttpUtility.HtmlEncode(target);
        }

        public static string HtmlDecode(this string target)
        {
            return HttpUtility.HtmlDecode(target);
        }
    }
}
