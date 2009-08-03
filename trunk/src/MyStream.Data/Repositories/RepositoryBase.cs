using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Configuration;

namespace MyStream.Data
{
    public class RepositoryBase
    {
        private const string DEFAULT_CONNECTION_STRING_NAME = "MyStreamConnectionString";

        public MyStreamDataContext GetDbInstance()
        {
            return new MyStreamDataContext(ConfigurationManager.ConnectionStrings[DEFAULT_CONNECTION_STRING_NAME].ConnectionString);
        }

        public virtual T Insert<T>(T obj) where T : class
        {
            using (var db = GetDbInstance())
            {
                db.GetTable<T>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return obj;
            }
        }

        public virtual T Insert<T>(Action<T> populate) where T : class, new()
        {
            using (var db = GetDbInstance())
            {
                return Insert<T>(new T(), populate);
            }
        }

        public virtual T Insert<T>(T obj, Action<T> populate) where T : class
        {
            using (var db = GetDbInstance())
            {
                populate(obj);
                return Insert<T>(obj);
            }
        }

        public virtual T Update<T>(T obj, Action<T> populate) where T : class
        {
            using (var db = GetDbInstance())
            {
                db.GetTable<T>().Attach(obj);
                populate(obj);
                db.SubmitChanges();
                return obj;
            }
        }

        public virtual T Update<T>(T obj) where T : class
        {
            using (var db = GetDbInstance())
            {
                db.GetTable<T>().Attach(obj);
                db.SubmitChanges();
                return obj;
            }
        }

        public virtual void Delete<T>(T obj, System.Data.Linq.DataContext db) where T : class
        {
            db.GetTable<T>().DeleteOnSubmit(obj);
            db.SubmitChanges();
        }
    }
}
