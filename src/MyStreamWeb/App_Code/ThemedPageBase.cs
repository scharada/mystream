using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MyStream.Business;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace MyStreamWeb
{
    public class ThemedPageBase : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            Theme = new Facade().GetSiteInfo().CurrentTheme;
            
            base.OnPreInit(e);
        }

        public static Control FindControlRecursive(Control Root, string Id)
        {
            if (Root.ID == Id)
                return Root;


            foreach (Control Ctl in Root.Controls)
            {
                Control FoundCtl = FindControlRecursive(Ctl, Id);
                if (FoundCtl != null)
                    return FoundCtl;
            }

            return null;
        }

        public static HtmlTableCell CreateTableCell(Control control)
        {
            var tdCell = new HtmlTableCell();
            tdCell.Controls.Add(control);
            return tdCell;
        }
    }
}
