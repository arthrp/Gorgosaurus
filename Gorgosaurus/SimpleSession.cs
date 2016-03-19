using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus
{
    public sealed class SimpleSession
    {
        //Some magick for the singleton pattern from Jon S.
        static SimpleSession()
        {
        }

        private SimpleSession()
        {
        }

        private static readonly SimpleSession _instance = new SimpleSession();
        private readonly int _expirationDays = Int32.Parse(ConfigurationManager.AppSettings["SessionExpiresDays"]);

        public static SimpleSession Instance
        {
            get { return _instance; }
        }

        public void Add(string key, object value)
        {
            var cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.Now.AddDays(_expirationDays) };
            cache.Add(key, value, policy);
        }

        public T Get<T>(string key) where T : class
        {
            if (String.IsNullOrEmpty(key))
                return null;

            ObjectCache cache = MemoryCache.Default;
            var res = cache.Get(key);

            if(res == null)
                return null;

            return res as T;
        }

        public string Get(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            var res = cache.Get(key);

            if (res == null)
                return null;

            return res as string;
        }

        public void Remove(string key)
        {
            ObjectCache cache = MemoryCache.Default;
            cache.Remove(key);
        }
    }
}
