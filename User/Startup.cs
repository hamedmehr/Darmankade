using Darmankade.Model.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class Startup
    {
        public static IServiceProvider Provider { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDBContext>(options => options.
                    UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "User", Version = "v1" });
            });

            services.AddScoped<IUserRepository<Darmankade.Model.Models.User>, UserRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserDBContext dbcontext, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "User v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            dbcontext.Database.Migrate();
            Provider = serviceProvider;
            Darmankade.ServiceBroker.RabbitMQHandler.ReceiveEvent("Darmankade", (m, e) =>
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<UserViewModel>(Encoding.UTF8.GetString(e.Body.ToArray()));
                //using (var service = Provider.CreateScope())
                //{
                //    service.ServiceProvider.GetService<IUserRepository<Darmankade.Model.Models.User>>().AddUser(user);
                //}
            });
        }
    }
}
