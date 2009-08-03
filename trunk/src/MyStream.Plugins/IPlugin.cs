using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Data;
using MyStream.Data.ObjectModel;

namespace MyStream.Plugins
{
    public interface IPlugin
    {
        List<StreamItem> Execute(Subscription subscription);
        string GetTypeName();
        string GetIconPath();
        string GetShortName();
        string GetFriendlyName();
        Subscription GetSubscriptionForAdd(Dictionary<string, string> parameters);
    }
}
