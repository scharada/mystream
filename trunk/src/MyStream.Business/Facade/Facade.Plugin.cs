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
        public IPlugin GetPluginFromSubscription(Subscription s)
        {
            return Plugins.AsParallel().SingleOrDefault<IPlugin>(p => p.GetTypeName() == s.Type);
        }

        public IPlugin LoadPlugin(string type)
        {
            return Plugins.AsParallel().SingleOrDefault<IPlugin>(p => p.GetTypeName() == type);
        }

        public bool Subscribe(IPlugin plugin, Dictionary<string, string> parameters)
        {
            var success = false;
            var subscription = plugin.GetSubscriptionForAdd(parameters);

            if (subscription != null)
            {
                SubscriptionRep.Insert(subscription);
                success = true;
            }

            return success; 
        }
    }
}
