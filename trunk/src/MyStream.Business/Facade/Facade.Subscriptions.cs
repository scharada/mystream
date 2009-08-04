// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Plugins;
using MyStream.Data;
using MyStream.Data.ObjectModel;
using System.Threading;

namespace MyStream.Business
{
    public partial class Facade
    {
        public List<Subscription> GetAllSubscriptions()
        {
            return SubscriptionRep.GetAll();
        }
        
        public List<IPlugin> GetAllSocialServices()
        {
            return IoC.ResolveAll<IPlugin>().ToList();
        }

        public bool DeleteSubscription(Guid guid)
        {
            return SubscriptionRep.Delete(guid);
        }
    }
}
