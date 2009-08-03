using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Data
{
    public class SubscriptionsRepository : RepositoryBase, ISubscriptionsRepository
    {
        private readonly MyStreamDataContext _dc;

        public SubscriptionsRepository()
        {
            _dc = GetDbInstance();
        }

        public List<Subscription> GetAll()
        {
            return (from s in _dc.Subscriptions.AsParallel() select s).ToList();
        }

        public Subscription Insert(Subscription s)
        {
            return Insert<Subscription>(s);
        }

        public bool Delete(Guid guid)
        {
            try
            {
                using(var db = GetDbInstance())
                {
                    var subscription = db.Subscriptions.SingleOrDefault(s => s.ID == guid);
                    if (subscription != null)
                    {
                        Delete<Subscription>(subscription, db);
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
