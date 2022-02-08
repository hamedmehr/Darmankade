using Ocelot.Cache;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Http;

namespace OcelotGateWay
{
    public class MyCaching : IOcelotCache<CachedResponse>
    {
        public IHttpContextAccessor HttpContextAccessor { get { return ServiceProviderHandler.GetService<IHttpContextAccessor>(); } }
        public IDistributedCache DistributedCache { get { return ServiceProviderHandler.GetService<IDistributedCache>(); } }
        private static ICacheKeyDecorator CacheKeyDecorator { get; set; }
        public MyCaching()
        {
            if (CacheKeyDecorator == null)
            {
                CacheKeyDecorator = new AuthorizationCacheKeyDecorator();
                CacheKeyDecorator.SetDecorator(new BodyCacheKeyDecorator());
            }
        }
        public void Add(string key, CachedResponse value, TimeSpan ttl, string region)
        {
            var mainKey = key;
            
            mainKey += CacheKeyDecorator.GetCacheKey(key,region,HttpContextAccessor.HttpContext);

            DistributedCache.SetObject(mainKey, value, ttl);
        }
        public void AddAndDelete(string key, CachedResponse value, TimeSpan ttl, string region)
        {
            var mainKey = key;

            mainKey += CacheKeyDecorator.GetCacheKey(key, region, HttpContextAccessor.HttpContext);

            DistributedCache.SetObject(mainKey, value, ttl);
        }

        public void ClearRegion(string region)
        {

        }

        public CachedResponse Get(string key, string region)
        {
            var response = default(CachedResponse);

            var mainKey = key;

            mainKey += CacheKeyDecorator.GetCacheKey(key, region, HttpContextAccessor.HttpContext);

            if (region.ToLower().Contains("checkauthforcache"))
            {
                bool authenticated = new CachedResponseAuthentication().Authenticatie();

                if (authenticated)
                    response = DistributedCache.GetObject<CachedResponse>(mainKey);

            }
            else
            {
                response = DistributedCache.GetObject<CachedResponse>(mainKey);
            }

            return response;
        }
    }
}
