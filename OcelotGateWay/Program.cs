using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using App.Metrics;
using App.Metrics.AspNetCore;
using App.Metrics.AspNetCore.Health;

namespace OcelotGateWay
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);
            builder.ConfigureServices(s => s.AddSingleton(builder))
                .ConfigureAppConfiguration(
                ic => ic
                .AddJsonFile(Path.Combine("configuration", "CoreRoute.json")))

                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, loggingbuilder) =>
                {
                    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    loggingbuilder.AddConsole();
                    loggingbuilder.AddDebug();
                })
                .ConfigureMetricsWithDefaults(appMetricsbuilder =>
                {
                    appMetricsbuilder.Report.ToInfluxDb("http://192.168.40.78:8086", "FrontAPI");
                })
            .UseMetrics()
            .UseHealth();
            IWebHost host = builder.Build();
            return host;
        }
    }
}
