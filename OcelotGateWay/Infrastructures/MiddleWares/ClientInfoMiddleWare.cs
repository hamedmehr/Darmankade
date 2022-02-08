using OcelotGateWay.DTOs.RequestLogDTOs;
using OcelotGateWay.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using System;
using System.Threading.Tasks;

namespace OcelotGateWay.Infrastructures.MiddleWares
{
    public static class ClientInfoMiddleWare
    {
        public static void SaveClientInfo(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                DateTime RequestDateTime = DateTime.Now;
                string RemoteIpAddress = context.Connection.RemoteIpAddress.ToString();
                string RemotePort = context.Connection.RemotePort.ToString();

                string Method = context.Request.Method;

                string AbsoluteUri = string.Concat(
                            context.Request.Scheme,
                            "://",
                            context.Request.Host.ToUriComponent(),
                            context.Request.PathBase.ToUriComponent(),
                            context.Request.Path.ToUriComponent(),
                            context.Request.QueryString.ToUriComponent());

                string UserAgent = context.Request.Headers["User-Agent"];

                string IMEI = context.Request.Headers["IMEI"];
                string Authorization = context.Request.Headers["Authorization"];

                //Do not use await!
                Task.Factory.StartNew(() => ServiceProviderHandler.GetService<IRequestLogService>().SaveLog(new SaveLogInputDTO()
                {
                    AbsoluteUri = AbsoluteUri,
                    Authorization = Authorization,
                    IMEI = IMEI,
                    Method = Method,
                    RemoteIpAddress = RemoteIpAddress,
                    RemotePort = RemotePort,
                    RequestDateTime = RequestDateTime,
                    UserAgent = UserAgent
                })
                );

                await next.Invoke();
            });
        }
    }
}
