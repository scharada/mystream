using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyStream.Data;
using MyStream.Business;

namespace MyStreamWeb.UserControls
{
    public partial class SubscriptionsSidebar : System.Web.UI.UserControl
    {
        public List<Subscription> Items = new List<Subscription>();

        protected void Page_Load(object sender, EventArgs e)
        {
            using (var facade = new Facade())
            {
                Items = facade.GetAllSubscriptions();
            }
        }
    }
}