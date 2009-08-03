using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MyStream.Data;

namespace MyStream.Business
{
    public partial class Facade
    {
        private void ReloadCurrentSiteInfo()
        {
            CurrentSiteInfo = GetSiteInfo();
        }

        public void CreateDefaultSiteInfo()
        {
            SiteInfoRep.Insert("Site Title Here", "Site Slogan Here", "password", 60, "Featuring");
            ReloadCurrentSiteInfo();
        }

        public bool IsValidAdmin(string password)
        {
            return CurrentSiteInfo.AdminPassword == password;
        }

        public void ChangePassword(string newPassword)
        {
            SiteInfoRep.Update(CurrentSiteInfo, info => info.AdminPassword = newPassword);
            ReloadCurrentSiteInfo();
        }

        public void UpdateSiteInfo(Action<SiteInfo> populate)
        {
            SiteInfoRep.Update(CurrentSiteInfo, populate);
            ReloadCurrentSiteInfo();
        }

        public SiteInfo GetSiteInfo()
        {
            return SiteInfoRep.Get();
        }
    }
}
