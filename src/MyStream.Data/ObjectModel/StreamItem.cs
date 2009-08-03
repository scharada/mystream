using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Data.ObjectModel
{
    public class StreamItem
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Timestamp { get; set; }
        public string Description { get; set; }
        public Guid SubscriptionID { get; set; }
    }
}
