using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Data;
using System.Xml.Linq;
using MyStream.Data.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data.Linq;

namespace MyStream.Plugins
{
    public class RssPlugin : IPlugin
    {
        public const string TYPE_NAME = "RSS_PLUGIN";
        public const string FRIENDLY_NAME = "RSS/Atom - Share your and favorite blogs with the world";
        private const string ICON_PATH = "Images/rss.png";
        private const string SHORT_NAME = "RSS/Atom";
        private Subscription _Subscription = null;

        private XElement _Feed;
        public XElement Feed
        {
            get
            {
                try
                {
                    _Feed = (_Feed == null && _Subscription != null && !string.IsNullOrEmpty(_Subscription.Url)) ? XElement.Load(_Subscription.Url) : _Feed;
                }
                catch (Exception)
                {
                    _Feed = null;
                }

                return _Feed;
            }
        }

        [PluginParameter(name: "url", friendlyName: "RSS/Atom URL", parameterType: PluginParameterType.Required)]
        public string RssAtomUrl { get; set; }

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

            try
            {
                if (Feed != null)
                {
                    XNamespace ns = "http://www.w3.org/2005/Atom";

                    // RSS
                    if (Feed.Element("channel") != null)
                    {
                        var items = (from item in Feed.Element("channel").Elements("item").AsParallel() select item).ToList();
                        Parallel.ForEach(items, i =>
                        {
                            var streamItem = new StreamItem();
                            streamItem.Timestamp = Utilities.Rfc822DateTime.FromString(i.Element("pubDate").Value);
                            streamItem.Url = i.Element("link").Value;
                            streamItem.Title = i.Element("title").Value;
                            streamItem.Icon = _Subscription.Icon;

                            if (i.Element("description") != null)
                                streamItem.Description = i.Element("description").Value;

                            list.Add(streamItem);
                        });
                    }

                    // Atom
                    else if (Feed.Element(ns + "entry") != null)
                    {
                        var items = (from item in Feed.Elements(ns + "entry").AsParallel() select item).ToList();
                        Parallel.ForEach(items, i =>
                        {
                            var streamItem = new StreamItem();
                            streamItem.Timestamp = Utilities.Rfc822DateTime.FromString(i.Element(ns + "published").Value);
                            streamItem.Url = i.Elements(ns + "link").Single(e => (e.Attribute("rel").Value == "alternate")).Attribute("href").Value;
                            streamItem.Title = i.Element(ns + "title").Value;
                            streamItem.Icon = _Subscription.Icon;

                            if (i.Element(ns + "content") != null)
                                streamItem.Description = i.Element(ns + "content").Value;

                            list.Add(streamItem);
                        });
                    }

                    // Invalid
                    else
                        list = null;

                }
            }
            catch (Exception e)
            { 
                // We got upset by the RSS/Atom, load previous items.
                // TODO: list = bla bla.
            }
            finally
            {
                if (list != null && list.Count > 0)
                {
                    Parallel.ForEach(list, i =>
                    {
                        if (i != null && !string.IsNullOrEmpty(i.Description))
                        {
                            var img = Regex.Match(i.Description, "src=[\"]?([^\" >]+)", RegexOptions.Compiled);
                            if (img.Success)
                                i.Description = "<img " + img.Value + "\" class=\"embedded-image\">";
                            else
                                i.Description = string.Empty;
                        }
                    });
                }
            }

            return list;
        }

        public string GetSubscriptionFriendlyName()
        {
            var friendlyName = string.Empty;

            if (Feed != null)
            {
                XNamespace ns = "http://www.w3.org/2005/Atom";

                // RSS
                if (Feed.Element("channel") != null)
                {
                    friendlyName = Feed.Element("channel").Element("title").Value;
                }
                else if (Feed.Element(ns + "entry") != null)
                {
                    friendlyName = Feed.Element(ns + "title").Value;
                }
            }

            return friendlyName;
        }

        public string GetSubscriptionFriendlyUrl()
        {
            var friendlyUrl = string.Empty;

            if (Feed != null)
            {
                XNamespace ns = "http://www.w3.org/2005/Atom";

                // RSS
                if (Feed.Element("channel") != null)
                {
                    friendlyUrl = Feed.Element("channel").Element("link").Value;
                }
                else if (Feed.Element(ns + "entry") != null)
                {
                    friendlyUrl = Feed.Elements(ns + "link").Single(i => i.Attribute("type") != null && i.Attribute("type").Value == "text/html").Attribute("href").Value;
                }
            }

            return friendlyUrl;
        }

        public Subscription GetSubscriptionForAdd(Dictionary<string, string> parameters)
        {
            var subscription = new Subscription
            {
                ID = Guid.NewGuid(),
                Type = GetTypeName(),
                Icon = ICON_PATH,
                UserName = string.Empty,
                Password = string.Empty,
                Url = parameters["url"],
                LastUpdated = DateTime.Now
            };

            _Subscription = subscription;
            var title = GetSubscriptionFriendlyName();
            subscription.Title = title.Substring(0, title.Length < 64 ? title.Length : 64);
            subscription.FriendlyUrl = GetSubscriptionFriendlyUrl();

            return Feed != null ? subscription : null;
        }

        public string GetShortName()
        {
            return SHORT_NAME;
        }
    }
}
