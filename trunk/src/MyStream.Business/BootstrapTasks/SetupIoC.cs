// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;

namespace MyStream.Business.BootstrapTasks
{
    public class SetupIoC : IBootstrapTasks
    {
        private IUnityContainer Container { get; set; }

        public SetupIoC(object additionalParameter = null)
        {
            if (additionalParameter != null)
            {
                Container = (IUnityContainer)additionalParameter;
            }
        }

        public void Run()
        {
            if (Container != null)
            {
                IoC.Run(Container);
            }
        }
    }
}
