// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.Unity;
using System.Diagnostics;
using MyStream.Data;
using MyStream.Utilities;
using MyStream.Plugins;
using System.Collections;

namespace MyStream.Business
{
    public class IoC
    {
        private static IUnityContainer _Container;

        static IoC()
        {
            try
            {
            }
            catch (ArgumentException)
            {
            }
        }

        public static void Run(IUnityContainer container)
        {
            _Container = container;

            // Repository Registration
            _Container.RegisterType<ISiteInfoRepository, SiteInfoRepository>()
                .RegisterType<IStreamDataRepository, StreamDataRepository>()
                .RegisterType<ISubscriptionsRepository, SubscriptionsRepository>()

            // Register plugins - this list will determine the order when shown as list 
                .RegisterType<IPlugin, RssPlugin>(RssPlugin.TYPE_NAME)
                .RegisterType<IPlugin, TwitterPlugin>(TwitterPlugin.TYPE_NAME)
                .RegisterType<IPlugin, DeliciousPlugin>(DeliciousPlugin.TYPE_NAME)
                .RegisterType<IPlugin, FlickrPlugin>(FlickrPlugin.TYPE_NAME);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type)
        {
            ArgValidator.IsNotNull(type, "type");
            return (T)_Container.Resolve(type);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(Type type, string name)
        {
            ArgValidator.IsNotNull(type, "type");
            ArgValidator.IsNotEmpty(name, "name");

            return (T)_Container.Resolve(type, name);
        }

        [DebuggerStepThrough]
        public static T Resolve<T>()
        {
            return _Container.Resolve<T>();
        }

        [DebuggerStepThrough]
        public static T Resolve<T>(string name)
        {
            ArgValidator.IsNotEmpty(name, "name");
            return _Container.Resolve<T>(name);
        }

        [DebuggerStepThrough]
        public static IEnumerable<T> ResolveAll<T>()
        {
            return _Container.ResolveAll<T>();
        }
    }
}
