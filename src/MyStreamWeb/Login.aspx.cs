// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyStream.Business;

namespace MyStreamWeb
{
    public partial class Login : ThemedPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (!string.IsNullOrEmpty(txtPassword.Text))
                {
                    using (var facade = new Facade())
                    {
                        lblIncorrectPass.Visible = !facade.Signin(txtPassword.Text);
                    }
                }
            }
        }
    }
}