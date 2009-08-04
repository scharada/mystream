// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Data;

namespace MyStream.Business.BootstrapTasks
{
    public class EnsureInitialData : IBootstrapTasks
    {
        public void Run()
        {
            using (var facade = new Facade())
            {
                var siteInfo = facade.GetSiteInfo();

                if (siteInfo == null)
                {
                    facade.CreateDefaultSiteInfo();
                }
            }
        }
    }
}
