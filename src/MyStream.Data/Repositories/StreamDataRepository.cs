// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Data
{
    public class StreamDataRepository : RepositoryBase, IStreamDataRepository
    {
        public StreamData Insert(StreamData s)
        {
            return GetDbInstance().StreamDatas.Count(i => s.Url == i.Url) > 0 ? Insert<StreamData>(s) : null;
        }

        public List<StreamData> InsertAll(List<StreamData> items)
        {
            var list = new List<StreamData>();

            items.ForEach(item =>
            {
                var data = Insert<StreamData>(item);

                if(data != null)
                    list.Add(data);
            });

            return list;
        }
    }
}
