using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyStream.Data
{
    public interface IRepository
    {
        T Insert<T>(T obj) where T : class;
        T Insert<T>(Action<T> populate) where T : class, new();
        T Insert<T>(T obj, Action<T> populate) where T : class;
    }
}
