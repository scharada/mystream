// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Plugins
{
    public enum PluginParameterType
    {
        Optional,
        Required
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class PluginParameterAttribute : Attribute
    {
        public PluginParameterType ParameterType { get; private set; }
        public string Name { get; private set; }
        public string FriendlyName { get; private set; }
        public string Description { get; private set; }

        public PluginParameterAttribute(string name = "", string friendlyName = "", string description = "", PluginParameterType parameterType = PluginParameterType.Optional)
        {
            Name = name;
            FriendlyName = friendlyName;
            Description = description;
            ParameterType = parameterType;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}", Name, FriendlyName, Description);
        }
    }
}
