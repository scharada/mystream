// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Html
{
    public static string MenuItem(string text, string url)
    {
        var builder = new System.Text.StringBuilder();

        if (HttpContext.Current.Request.RawUrl.EndsWith(url, StringComparison.CurrentCultureIgnoreCase) || 
            HttpContext.Current.Request.RawUrl.EndsWith(url + "?", StringComparison.CurrentCultureIgnoreCase))
        {
            builder.Append("<li class=\"active\">");
        }
        else
        {
            builder.Append("<li>");
        }

        builder.Append(string.Format("<a href=\"{0}\"><span>{1}</span></a></li>", url, text));

        return builder.ToString();
    }

    public static string AnchorWithIcon(string text, string url, string icon)
    {
        return string.Format("<img src=\"{2}\" class=\"item-icon\" style=\"float: none;\" /> <a href=\"{0}\" style=\"vertical-align: super;\">{1}</a>", url, text, icon);
    }
}