// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;

namespace MyStream.Business.BootstrapTasks
{
    public class Bootstrapper
    {
        public static void Run(IUnityContainer container)
        {
            var list = new List<IBootstrapTasks>()
            {
                new SetupIoC(container),
                new EnsureInitialData()
            };

            list.ForEach(h => h.Run());
        }
    }
}
