using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Polly;
using Ocelot.Cache.CacheManager;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Ocelot.Middleware;
using Ocelot.Cache;
using Microsoft.AspNetCore.Mvc;
using OcelotGateWay.Repository.Interfaces;
using OcelotGateWay.Repository;
using OcelotGateWay.Infrastructures.MiddleWares;
using OcelotGateWay.Services.Interfaces;
using OcelotGateWay.Services;

namespace OcelotGateWay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //App Metrics
            services.AddMvc().AddMetrics();

            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());
            if (bool.Parse(Configuration["AllowAnyOrigin"].ToString()))
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowCredentials());
                });
            }
            else
            { 
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(Configuration.GetSection("AllowOrigins").Get<string[]>())
                        .AllowCredentials());
                });
            }

            services
            .AddOcelot(Configuration)
            .AddPolly();//.AddCacheManager(x => { x.WithDictionaryHandle().Build(); });

            //services.AddDistributedRedisCache(options =>
            //{
            //    options.Configuration = Configuration["RedisCnnString"];
            //});

            #region IoC

            services.AddSingleton<IOcelotCache<CachedResponse>, MyCaching>();

            services.AddTransient<IRequestLogRepository, RequestLogRepository>();
            services.AddTransient<IRequestLogService, RequestLogService>();

            #endregion

            ServiceProviderHandler.Initialize(services.BuildServiceProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var pathBase = Configuration["PATH_BASE"];

            if (!string.IsNullOrEmpty(pathBase))
            {
                app.UsePathBase(pathBase);
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.SaveClientInfo();

            app.UseMiddleware<CustomMiddleware>();

            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });

            app.UseCors("CorsPolicy");

            app.UseOcelot().Wait();
        }
    }
}
