using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;

namespace DataModel.Master
{
    public class CacheHelper
    {
        public static void SaveTocache(string cacheKey, object savedItem, DateTime absoluteExpiration)
        {

            MemoryCache.Default.Add(cacheKey, savedItem, absoluteExpiration);
            try
            {
                TokenKeys obj = (TokenKeys)savedItem;
                TokenKeys oldtokenkey = (from n in MemoryCache.Default select (TokenKeys)n.Value).ToList().Where(m => m.token != obj.token && m.USERID == obj.USERID).FirstOrDefault();
                if (oldtokenkey != null) { RemoveFromCache(oldtokenkey.token); }
            }
            catch (Exception ex)
            {

            }
        }

        public static TokenKeys GetFromCache(string cacheKey)
        {

            return (TokenKeys)MemoryCache.Default[cacheKey];
        }

        public static void RemoveFromCache(string cacheKey)
        {
            MemoryCache.Default.Remove(cacheKey);
        }

        public static bool IsIncache(string cacheKey)
        {
            return MemoryCache.Default[cacheKey] != null;
        }

        public static void SaveClientDB(string cacheKey, object savedItem, DateTime absoluteExpiration)
        {
            MemoryCache.Default.Add(cacheKey, savedItem, absoluteExpiration);
        }
    }
}
