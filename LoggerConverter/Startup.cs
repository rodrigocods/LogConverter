using LoggerConverter.Data;
using LoggerConverter.Middlewares;
using LoggerConverter.Repository;
using LoggerConverter.Repository.Interfaces;
using LoggerConverter.Services;
using LoggerConverter.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LoggerConverter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddHttpClient();

            services.AddDbContext<LogContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("LogDatabase")));

            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<ILogConvertedRepository, LogConvertedRepository>();

            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseErrorHandlerMiddleware();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
