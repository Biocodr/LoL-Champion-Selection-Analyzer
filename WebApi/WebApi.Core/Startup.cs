﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.DataAccess;
using WebApi.DataAccess.Repositories;
using WebApi.DataAccess.Repositories.Interfaces;

namespace WebApi.Core
{
    public class Startup
    {
        // TODO use multiple environments https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc(); // TODO remove, since MVC is not used

            // Database contexts
            services.AddDbContexts(Configuration.GetConnectionString("DefaultConnection"));

            // Repositories
            services.AddScoped<ILeagueRepository, LeagueRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc(); // TODO remove, since MVC is not used

            //DbInitializer.Initialize(context);
        }
    }
}
