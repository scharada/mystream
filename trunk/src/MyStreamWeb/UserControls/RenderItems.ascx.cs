// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyStream.Business;
using MyStream.Data.ObjectModel;

namespace MyStreamWeb.UserControls
{
    public partial class RenderItems : System.Web.UI.UserControl
    {
        public List<StreamItem> Items = new List<StreamItem>();

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var facade = new Facade())
            {
                Items = facade.GetStreamItems();
            }
        }

        public string RenderTitle(string title, string url = "")
        {
            return !string.IsNullOrEmpty(url) ? "<a href=\"" + url + "\">" + title + "</a>" : title;
        }
    }
}