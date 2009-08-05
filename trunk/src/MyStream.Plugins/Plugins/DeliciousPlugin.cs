// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Data;
using System.Xml.Linq;
using MyStream.Data.ObjectModel;

namespace MyStream.Plugins
{
    public sealed class DeliciousPlugin : IPlugin
    {
        public const string TYPE_NAME = "DELICIOUS_PLUGIN";
        private const string ICON_PATH = "Images/delicious.png";
        public const string FRIENDLY_NAME = "Delicious - Share your public bookmarks";
        private const string SHORT_NAME = "Delicious";
        private Subscription _Subscription = null;

        [PluginParameter(name: "delicious_username", friendlyName: "Delicious Username", parameterType: PluginParameterType.Required)]
        public string DeliciousUserName { get; set; }

        private readonly RssPlugin _TheRssPlugin = new RssPlugin();

        public string GetTypeName()
        {
            return TYPE_NAME;
        }

        public string GetIconPath()
        {
            return ICON_PATH;
        }

        public string GetFriendlyName()
        {
            return FRIENDLY_NAME;
        }

        public List<StreamItem> Execute(Subscription subscription)
        {
            _Subscription = subscription;

            var rss = new RssPlugin();
            return rss.Execute(_Subscription);
        }

        public Subscription Subscribe(Dictionary<string, string> parameters)
        {
            parameters["url"] = "http://feeds.delicious.com/v2/rss/" + parameters["delicious_username"] + "?count=15";
            var subscription = _TheRssPlugin.Subscribe(parameters);

            subscription.Icon = ICON_PATH;
            subscription.Type = GetTypeName();

            return subscription;
        }

        public string GetShortName()
        {
            return SHORT_NAME;
        }
    }
}
