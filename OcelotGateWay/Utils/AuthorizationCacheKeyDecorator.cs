using Microsoft.AspNetCore.Http;

namespace OcelotGateWay
{
    public class AuthorizationCacheKeyDecorator : ICacheKeyDecorator
    {
        private ICacheKeyDecorator cacheKeyDecorator;
        public string GetCacheKey(string key, string region, HttpContext httpContext)
        {
            if (!string.IsNullOrEmpty(region) && (region == "WithAuth" || region == "Auth" || region == "WithAuthAndBody"))
            {
                var headers = httpContext.Request.Headers;

                if (headers.ContainsKey("Authorization"))
                    key += headers["Authorization"].ToString();

                if (cacheKeyDecorator != null)
                {
                    key += cacheKeyDecorator.GetCacheKey(key, region, httpContext);
                }
            }

            return key;
        }

        public void SetDecorator(ICacheKeyDecorator decorator)
        {
            cacheKeyDecorator = decorator;
        }
    }
}
