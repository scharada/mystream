// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Utilities
{
    public static class DateTimeExtensions
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        public static string ToRelative(this DateTime target)
        {
            target = target.ToUniversalTime();
            ArgValidator.IsNotInFuture(target, "target");
            ArgValidator.IsNotInvalidDate(target, "target");

            StringBuilder result = new StringBuilder();
            TimeSpan diff = (DateTime.UtcNow - target.ToUniversalTime());

            Action<int, string> format = (v, u) =>
            {
                if (v > 0)
                {
                    if (result.Length > 0)
                    {
                        result.Append(", ");
                    }

                    string plural = (v > 1) ? "s" : string.Empty;

                    result.Append("{0} {1}{2}".FormatWith(v, u, plural));
                }
            };

            if (diff.TotalDays > 365)
            {
                var year = (int)(diff.TotalDays / 365);
                format(year, "year");
                diff = diff.Subtract(TimeSpan.FromDays(year * 365));
            }

            format(diff.Days, "day");
            format(diff.Hours, "hour");
            format(diff.Minutes, "minute");

            return (result.Length == 0) ? "few moments" : result.ToString();
        }
    }
}
