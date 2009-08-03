using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Data;
using System.Xml.Linq;
using MyStream.Data.ObjectModel;
using System.Text.RegularExpressions;
using MyStream.Utilities;

namespace MyStream.Plugins
{
    public class FlickrPlugin : IPlugin
    {
        public const string TYPE_NAME = "FLICKR_PLUGIN";
        public const string FRIENDLY_NAME = "Flickr - Show your recent photos";
        private const string ICON_PATH = "Images/flickr.png";
        private const string SHORT_NAME = "Flickr";
        private Subscription _Subscription = null;
        private readonly RssPlugin _TheRssPlugin = new RssPlugin();

        [PluginParameter(name: "flickr_username", friendlyName: "Flickr Username", parameterType: PluginParameterType.Required)]
        public string FlickrUserName { get; set; }

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

        public Subscription GetSubscriptionForAdd(Dictionary<string, string> parameters)
        {
            Subscription subscription = null;
            var url = "http://www.flickr.com/photos/" + parameters["flickr_username"];

            var content = WebHelper.DownloadString(url);

            var matches = Regex.Matches(content, @"<(link)\s*[^>]*(title)=""(?<title>([^""]*))""\s*[^>]*(href)="""
                + @"(?<href>([^""]*))""", RegexOptions.Compiled);

            if (matches.Count > 0)
            {
                parameters["url"] = matches[0].Groups["href"].Value;    
                subscription = _TheRssPlugin.GetSubscriptionForAdd(parameters);

                if (subscription != null)
                {
                    subscription.Icon = ICON_PATH;
                    subscription.Type = GetTypeName();
                }
            }

            return subscription;
        }

        public string GetShortName()
        {
            return SHORT_NAME;
        }
    }
}
