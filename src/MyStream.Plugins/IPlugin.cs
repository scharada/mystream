// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

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
        Subscription Subscribe(Dictionary<string, string> parameters);
    }
}
