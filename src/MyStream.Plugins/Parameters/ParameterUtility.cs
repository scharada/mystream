// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace MyStream.Plugins
{
    public static class ParameterUtility
    {
        public static List<PluginParameterAttribute> GetParameters(IPlugin plugin)
        {
            var list = new List<PluginParameterAttribute>();
            var type = plugin.GetType();
            var fields = type.GetFields();

            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                object[] attrs = property.GetCustomAttributes(typeof(PluginParameterAttribute), false);
                if (attrs.Count() > 0)
                {
                    list.Add(((PluginParameterAttribute[])attrs).Single());
                }
            }

            return list;
        }
    }
}
