using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Utilities
{
    public class Rfc822DateTime
    {
        static public System.DateTime FromString(string date)
        {
            System.DateTime dt;
            int pos = date.LastIndexOf(" ");

            try
            {
                dt = Convert.ToDateTime(date);
                if (date.Substring(pos + 1) == "Z")
                {
                    dt = dt.ToUniversalTime();
                }
                else if (date.Substring(pos + 1) == "GMT")
                {
                    dt = dt.ToUniversalTime();
                }
                return dt;
            }
            catch (System.Exception x)
            {
                System.Diagnostics.Trace.WriteLine(x.Message);
            }

            dt = Convert.ToDateTime(date.Substring(0, pos));
            if (date[pos + 1] == '+')
            {
                int h = Convert.ToInt32(date.Substring(pos + 2, 2));
                dt = dt.AddHours(-h);
                int m = Convert.ToInt32(date.Substring(pos + 4, 2));
                dt = dt.AddMinutes(-m);
            }
            else if (date[pos + 1] == '-')
            {
                int h = Convert.ToInt32(date.Substring(pos + 2, 2));
                dt = dt.AddHours(h);
                int m = Convert.ToInt32(date.Substring(pos + 4, 2));
                dt = dt.AddMinutes(m);
            }
            else if (date.Substring(pos + 1) == "A")
            {
                dt = dt.AddHours(1);
            }
            else if (date.Substring(pos + 1) == "B")
            {
                dt = dt.AddHours(2);
            }
            else if (date.Substring(pos + 1) == "C")
            {
                dt = dt.AddHours(3);
            }
            else if (date.Substring(pos + 1) == "D")
            {
                dt = dt.AddHours(4);
            }
            else if (date.Substring(pos + 1) == "E")
            {
                dt = dt.AddHours(5);
            }
            else if (date.Substring(pos + 1) == "F")
            {
                dt = dt.AddHours(6);
            }
            else if (date.Substring(pos + 1) == "G")
            {
                dt = dt.AddHours(7);
            }
            else if (date.Substring(pos + 1) == "H")
            {
                dt = dt.AddHours(8);
            }
            else if (date.Substring(pos + 1) == "I")
            {
                dt = dt.AddHours(9);
            }
            else if (date.Substring(pos + 1) == "K")
            {
                dt = dt.AddHours(10);
            }
            else if (date.Substring(pos + 1) == "L")
            {
                dt = dt.AddHours(11);
            }
            else if (date.Substring(pos + 1) == "M")
            {
                dt = dt.AddHours(12);
            }
            else if (date.Substring(pos + 1) == "N")
            {
                dt = dt.AddHours(-1);
            }
            else if (date.Substring(pos + 1) == "O")
            {
                dt = dt.AddHours(-2);
            }
            else if (date.Substring(pos + 1) == "P")
            {
                dt = dt.AddHours(-3);
            }
            else if (date.Substring(pos + 1) == "Q")
            {
                dt = dt.AddHours(-4);
            }
            else if (date.Substring(pos + 1) == "R")
            {
                dt = dt.AddHours(-5);
            }
            else if (date.Substring(pos + 1) == "S")
            {
                dt = dt.AddHours(-6);
            }
            else if (date.Substring(pos + 1) == "T")
            {
                dt = dt.AddHours(-7);
            }
            else if (date.Substring(pos + 1) == "U")
            {
                dt = dt.AddHours(-8);
            }
            else if (date.Substring(pos + 1) == "V")
            {
                dt = dt.AddHours(-9);
            }
            else if (date.Substring(pos + 1) == "W")
            {
                dt = dt.AddHours(-10);
            }
            else if (date.Substring(pos + 1) == "X")
            {
                dt = dt.AddHours(-11);
            }
            else if (date.Substring(pos + 1) == "Y")
            {
                dt = dt.AddHours(-12);
            }
            else if (date.Substring(pos + 1) == "EST")
            {
                dt = dt.AddHours(5);
            }
            else if (date.Substring(pos + 1) == "EDT")
            {
                dt = dt.AddHours(4);
            }
            else if (date.Substring(pos + 1) == "CST")
            {
                dt = dt.AddHours(6);
            }
            else if (date.Substring(pos + 1) == "CDT")
            {
                dt = dt.AddHours(5);
            }
            else if (date.Substring(pos + 1) == "MST")
            {
                dt = dt.AddHours(7);
            }
            else if (date.Substring(pos + 1) == "MDT")
            {
                dt = dt.AddHours(6);
            }
            else if (date.Substring(pos + 1) == "PST")
            {
                dt = dt.AddHours(8);
            }
            else if (date.Substring(pos + 1) == "PDT")
            {
                dt = dt.AddHours(7);
            }

            return dt;
        }
    }

}
