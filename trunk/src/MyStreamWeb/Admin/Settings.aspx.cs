// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyStream.Business;
using System.Configuration;

namespace MyStreamWeb.Admin
{
    public partial class Settings : ThemedPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                using (var facade = new Facade())
                {
                    var changesSaved = false;
                    var info = facade.CurrentSiteInfo;

                    if (!string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text != facade.CurrentSiteInfo.AdminPassword)
                    {
                        facade.ChangePassword(txtPassword.Text);
                        changesSaved = true;
                    }

                    if (txtTitle.Text != facade.CurrentSiteInfo.SiteTitle)
                    {
                        facade.UpdateSiteInfo(i => i.SiteTitle = txtTitle.Text);
                        changesSaved = true;
                    }

                    if (txtSlogan.Text != facade.CurrentSiteInfo.SiteSlogan)
                    {
                        facade.UpdateSiteInfo(i => i.SiteSlogan = txtSlogan.Text);
                        changesSaved = true;
                    }

                    var result = int.MinValue;
                    if (int.TryParse(txtCacheDuration.Text, out result))
                    {
                        if (result > 0)
                        {
                            facade.UpdateSiteInfo(i => i.CacheDuration = result);
                            changesSaved = true;
                        }
                    }
                    

                    lblChangesSaved.Visible = changesSaved;
                }
            }
            else
            {
                LoadControls();
            }
        }

        private void LoadControls()
        {
            using (var facade = new Facade())
            {
                txtTitle.Text = facade.CurrentSiteInfo.SiteTitle;
                txtSlogan.Text = facade.CurrentSiteInfo.SiteSlogan;

                var test = ConfigurationManager.AppSettings["Themes"].Split("|".ToCharArray()); 
                ddlThemes.DataSource = test;
                ddlThemes.DataBind();
                ddlThemes.SelectedValue = facade.CurrentSiteInfo.CurrentTheme;

                txtCacheDuration.Text = facade.CurrentSiteInfo.CacheDuration.ToString();
            }
        }
    }
}