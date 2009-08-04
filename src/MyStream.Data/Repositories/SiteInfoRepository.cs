// Copyright (c) Tanzim Saqib. All rights reserved.
// For continued development and updates, visit http://TanzimSaqib.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Data
{
    public class SiteInfoRepository : RepositoryBase, ISiteInfoRepository
    {
        private readonly MyStreamDataContext _dc;

        public SiteInfoRepository()
        {
            _dc = GetDbInstance();
        }

        public SiteInfo Get()
        {
            return _dc.SiteInfos.AsParallel().FirstOrDefault();
        }

        public SiteInfo Insert(string title, string slogan, string password, int cacheDuration, string currentTheme)
        {
            return Insert<SiteInfo>((s) =>
                {
                    s.SiteTitle = title;
                    s.SiteSlogan = slogan;
                    s.AdminPassword = password;
                    s.CacheDuration = cacheDuration;
                    s.CurrentTheme = currentTheme;
                });
        }

        public SiteInfo Update(SiteInfo info, Action<SiteInfo> populate)
        {
            return Update<SiteInfo>(info, populate);
        }
    }
}
