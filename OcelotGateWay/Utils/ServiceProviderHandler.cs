using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace OcelotGateWay
{
    public class ServiceProviderHandler
    {
        public static void Initialize(IServiceProvider castleServiceContainerl)
        {
            CastleServiceContainer = castleServiceContainerl;
        }

        public static TService GetService<TService>()
        {
            return CastleServiceContainer.GetService<TService>();
        }

        public static IEnumerable<TService> GetServices<TService>()
        {
            return CastleServiceContainer.GetServices<TService>();
        }

        public static object GetService(Type TService)
        {
            return CastleServiceContainer.GetService(TService);
        }
        private static IServiceProvider CastleServiceContainer { get; set; }
    }
}
