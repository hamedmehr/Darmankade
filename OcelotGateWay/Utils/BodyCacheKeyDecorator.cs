using Microsoft.AspNetCore.Http;

namespace OcelotGateWay
{
    public class BodyCacheKeyDecorator : ICacheKeyDecorator
    {
        private ICacheKeyDecorator cacheKeyDecorator;
        private IHttpContextAccessor HttpContextAccessor { get { return ServiceProviderHandler.GetService<IHttpContextAccessor>(); } }
        public string GetCacheKey(string key, string region, HttpContext httpContext)
        {
            if (!string.IsNullOrEmpty(region) && (region == "Body" || region == "WithAuthAndBody"))
            {
                key += HttpContextAccessor.HttpContext.Items["RequestBody"].ToString();

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
