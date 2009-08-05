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
using System.Web.Security;

namespace MyStream.Business
{
    public partial class Facade : IDisposable
    {
        private object _lock = new object();
        public IEnumerable<IPlugin> Plugins { get; private set; }
        public SiteInfo CurrentSiteInfo { get; private set; }
        public System.Web.HttpContext Context { get; private set; }

        public ISiteInfoRepository SiteInfoRep { get; private set; }
        public ISubscriptionsRepository SubscriptionRep { get; private set; }
        public IStreamDataRepository StreamDataRep { get; private set; }

        private Facade(ISiteInfoRepository siteInfoRep, ISubscriptionsRepository subscriptionRep, IStreamDataRepository streamDataRep)
        {
            SiteInfoRep = siteInfoRep;
            SubscriptionRep = subscriptionRep;
            StreamDataRep = streamDataRep;

            Plugins = IoC.ResolveAll<IPlugin>();
        }

        public Facade() : 
            this(IoC.Resolve<ISiteInfoRepository>(), IoC.Resolve<ISubscriptionsRepository>(), IoC.Resolve<IStreamDataRepository>())
        {
            if (CurrentSiteInfo == null)
            {
                ReloadCurrentSiteInfo();
            }

            Context = System.Web.HttpContext.Current;
        }

        public void Dispose()
        {
        }

        public List<StreamItem> GetStreamItems()
        {
            var list = new System.Collections.Concurrent.ConcurrentBag<StreamItem>();
            var subscriptions = GetAllSubscriptions();

            Parallel.ForEach(subscriptions, s =>
            {
                var plugin = GetPluginFromSubscription(s);
                var items = new List<StreamItem>();
                var cachedItems = Context.Cache[s.ID.ToString()];

                items = cachedItems != null ? (List<StreamItem>)cachedItems : plugin.Execute(s);

                if (items != null && items.Count > 0)
                {
                    Context.Cache.Add(s.ID.ToString(), items.ToList(), null,
                        DateTime.Now.AddSeconds(CurrentSiteInfo.CacheDuration),
                        System.Web.Caching.Cache.NoSlidingExpiration,
                        System.Web.Caching.CacheItemPriority.Normal, null);

                    items.ForEach(item => list.Add(item));
                }
            });

            return list.Where(i => i != null && i.Timestamp != null)
                .OrderByDescending(i => i.Timestamp).ToList();
        }

        private void SaveStreamItems(List<StreamItem> items)
        {
            //StreamDataRep.InsertAll(
            //    (from i in items 
            //     select new StreamData 
            //     { 
            //         SubscriptionID = i.SubscriptionID, 
            //         Url = i.Url,
            //         Title = i.Title, 
            //         Description = i.Description, 
            //         Timestamp = DateTime.Now 
            //     }).ToList());
        }

        public bool Signin(string password)
        {
            if (CurrentSiteInfo.AdminPassword == password)
            {
                FormsAuthentication.RedirectFromLoginPage("admin", false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
            Context.Response.Redirect("Default.aspx");
        }
    }
}
