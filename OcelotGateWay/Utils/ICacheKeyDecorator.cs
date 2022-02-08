using Microsoft.AspNetCore.Http;

namespace OcelotGateWay
{
    public interface ICacheKeyDecorator
    {
        string GetCacheKey(string key, string region, HttpContext httpContext);

        void SetDecorator(ICacheKeyDecorator decorator);
    }
}
