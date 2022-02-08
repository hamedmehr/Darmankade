using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
namespace OcelotGateWay
{
    public static class CahceHelper
    {
        public static void SetObject(this IDistributedCache distributedCache, string cacheKey, object data, TimeSpan ttl)
        {
            var serialized = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    TypeNameHandling = TypeNameHandling.All
                });

            distributedCache.SetStringAsync(cacheKey, serialized,
                     new DistributedCacheEntryOptions()
                     {
                         AbsoluteExpirationRelativeToNow = ttl
                     });
        }
        public static T GetObject<T>(this IDistributedCache distributedCache, string cacheKey) where T : class
        {
            T result = default(T);

            var outPutResult = distributedCache.GetStringAsync(cacheKey).Result;

            if (string.IsNullOrEmpty(outPutResult) || outPutResult.Length == 0)
                return default(T);

            var deserialized = JsonConvert.DeserializeObject<T>(outPutResult, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.All
            });

            result = deserialized;
            //  var deserialized = JsonConvert.DeserializeObject<T>(result);


            return result;

        }
    }
}
