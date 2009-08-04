// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Data;
using System.Xml.Linq;
using MyStream.Data.ObjectModel;
using MyStream.Utilities;
using System.Text.RegularExpressions;

namespace MyStream.Plugins
{
    public class TwitterPlugin : IPlugin
    {
        public const string TYPE_NAME = "TWITTER_PLUGIN";
        private const string ICON_PATH = "Images/Twitter.png";
        public const string FRIENDLY_NAME = "Twitter - Publish your statuses";
        private const string SHORT_NAME = "Twitter";
        private Subscription _Subscription = null;
        private readonly RssPlugin _TheRssPlugin = new RssPlugin();

        [PluginParameter(name: "twitter_username", friendlyName: "Twitter Username", parameterType: PluginParameterType.Required)]
        public string TwitterUserName { get; set; }

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

            var list = new List<StreamItem>();

            // http://twitter.com/users/show/TanzimSaqib.xml
            ///var request =  "http://twitter.com/statuses/user_timeline/5704372.atom";
            /* "http://search.twitter.com/search.atom?q=from:" + _Subscription.UserName; */ 

            try
            {
                var feed = XElement.Load(subscription.Url);

                if (feed != null)
                {
                    // RSS
                    if (feed.Element("channel") != null)
                    {
                        list = (from item in feed.Element("channel").Elements("item").AsParallel()
                                select new StreamItem
                                {
                                    //Description = item.Elements(ns + "link").Single(e => (e.Attribute("rel").Value == "image")).Attribute("href").Value
                                    Title = XmlHelper.StripTags(item.Element("title").Value, 160).ToTweet("embedded-anchor"),
                                    Url = item.Element("link").Value,
                                    Icon = _Subscription.Icon,
                                    Timestamp = Utilities.Rfc822DateTime.FromString((item.Element("pubDate").Value))
                                }).ToList();
                    }
                    // Invalid
                    else
                        list = null;
                }
            }
            catch (Exception e)
            {
                // We got upset by the Twitter API at the moment, show from Db
            }
            finally
            {
            }

            return list;
        }

        public Subscription Subscribe(Dictionary<string, string> parameters)
        {
            Subscription subscription = null;
            var url = "http://twitter.com/" + parameters["twitter_username"];

            var content = WebHelper.DownloadString(url);

            var matches = Regex.Matches(content, @"<(link)\s*[^>]*(href)=""(?<href>([^""]*))""\s*[^>]*(title)="""
                + @"(?<title>([^""]*))""", RegexOptions.Compiled);

            if (matches.Count > 0)
            {
                parameters["url"] = matches[0].Groups["href"].Value;
                subscription = _TheRssPlugin.Subscribe(parameters);
                subscription.Icon = ICON_PATH;
                subscription.Type = GetTypeName();
            }

            return subscription;
        }

        public string GetShortName()
        {
            return SHORT_NAME;
        }
    }
}
