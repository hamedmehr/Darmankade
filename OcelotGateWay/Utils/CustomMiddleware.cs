using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Internal;
using System.Threading.Tasks;

namespace OcelotGateWay
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IHttpContextAccessor HttpContextAccessor)
        {
            var request = httpContext.Request;
            var headers = request.Headers;
            var content = "";

            request.EnableRewind();

            // Arguments: Stream, Encoding, detect encoding, buffer size 
            // AND, the most important: keep stream opened
            using (StreamReader reader
                      = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
            {
                content = reader.ReadToEnd();
            }

            // Rewind, so the core is not lost when it looks the body for the request
            request.Body.Position = 0;

            HttpContextAccessor.HttpContext.Items["RequestBody"] = content; ;

            await _next(httpContext);
        }
    }
}
