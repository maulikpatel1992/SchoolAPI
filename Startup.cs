using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System.IO;
using SchoolAPI.Extensions;
using AutoMapper;

namespace SchoolAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.ConfigureSwagger();
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureResponseCaching();
            services.ConfigureHttpCacheHeaders();

            //services.AddAuthentication();
            //services.ConfigureIdentity();
            //services.ConfigureJWT(Configuration);

            services.Configure<ApiBehaviorOptions>(options => 
            { 
                options.SuppressModelStateInvalidFilter = true; 
            });

            services.AddControllers(config => 
            { 
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
            }).AddNewtonsoftJson()
                .AddXmlDataContractSerializerFormatters();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseResponseCaching();
            app.UseHttpCacheHeaders();
            app.UseRouting();

            //app.UseAuthentication();
            app.UseAuthorization(); 
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(); 
            app.UseSwaggerUI(s => 
            { 
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "School API v1");
            });
        }
    }
}
