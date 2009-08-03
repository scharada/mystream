using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

using Microsoft.Practices.Unity;

namespace MyStreamWeb
{
    public class Global : System.Web.HttpApplication
    {
        private static IUnityContainer _Container;
        public static IUnityContainer Container
        {
            get { return _Container; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            if (Container == null)
                _Container = new UnityContainer();

            MyStream.Business.BootstrapTasks.Bootstrapper.Run(_Container);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}