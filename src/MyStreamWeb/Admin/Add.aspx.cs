using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MyStream.Business;
using System.Web.UI.HtmlControls;
using MyStream.Plugins;

namespace MyStreamWeb.Admin
{
    public partial class Add : ThemedPageBase
    {
        protected IPlugin _CurrentPlugin = null;
        protected List<PluginParameterAttribute> _ParameterProperties = null;
        protected string _ParametersList = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            var type = Request["Type"];

            if (!string.IsNullOrEmpty(type))
            {
                using(var facade = new Facade())
                {
                    _CurrentPlugin = facade.LoadPlugin(type); 

                    if(_CurrentPlugin == null)
                        Response.Redirect("Default.aspx");

                    _ParameterProperties = ParameterUtility.GetParameters(_CurrentPlugin);
                    _ParameterProperties.ForEach(p => _ParametersList += p.ToString() + "|");
                    _ParametersList = _ParametersList.TrimEnd("|".ToCharArray());                        
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

            if (IsPostBack)
            {
                var cookie = Request.Cookies["mystream_subscription_add_cookie"];

                if (cookie != null)
                {
                    var parameters = cookie.Value.Split("|".ToCharArray());
                    var dictionary = new Dictionary<string, string>();

                    foreach (var parameter in parameters)
                    {
                        var paramName = parameter.Split("=".ToCharArray())[0];
                        var paramValue = parameter.Split("=".ToCharArray())[1];
                        dictionary.Add(paramName, paramValue);
                    }

                    using (var facade = new Facade())
                    {
                        pnlSuccess.Visible = !(pnlInvalidInfo.Visible = !facade.Subscribe(_CurrentPlugin, dictionary));
                    }
                }
                else
                {
                    // Err occured
                }
            }
            else
            {
                CreateControls();
            }
        }

        public void CreateControls()
        {
            // Render Text inputs
            _ParameterProperties.ForEach(p =>
            {
                var tr = new HtmlTableRow();
                tr.Cells.Add(CreateTableCell(new LiteralControl(p.FriendlyName + ": ")));
                tr.Cells.Add(CreateTableCell(new LiteralControl("<input type=\"text\" style=\"width: 300px\" id=\"" + p.Name + "\" />")));

                tabForm.Controls.Add(tr);
            });


            // Render validation area
            var trValidation = new HtmlTableRow();
            trValidation.Cells.Add(CreateTableCell(new LiteralControl()));
            trValidation.Cells.Add(CreateTableCell(new LiteralControl("<span class=\"error-validation\" id=\"spnErrorMessage\"></span>")));
            tabForm.Controls.Add(trValidation);


            // Submit Button
            var trSubmit = new HtmlTableRow();
            var submitButton = new HtmlInputButton()
            {
                Value = "Subscribe"
            };

            submitButton.Attributes.Add("class", "button");
            submitButton.Attributes.Add("onclick", "validateInput();");
            submitButton.Attributes.Add("type", "button");

            trSubmit.Cells.Add(CreateTableCell(new LiteralControl()));

            var panel = new Panel();
            panel.Controls.Add(submitButton);

            var goBack = new LiteralControl("&nbsp;or <a href=\"javascript:history.go(-1);\">cancel</a>");
            panel.Controls.Add(goBack);

            trSubmit.Cells.Add(CreateTableCell(panel));
            tabForm.Controls.Add(trSubmit);
        }
    }
}