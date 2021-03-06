﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmitUp.Api.AutoMapper;
using SmitUp.Infra.CrossCutting.Identity.Data;
using SmitUp.Infra.CrossCutting.Identity.Security.Extensions;
using SmitUp.Infra.CrossCutting.IoC;
using SmitUp.Infra.Data.Context;

namespace SmitUp.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            ConfigureContexts(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();

            services.AddMediatR(typeof(Startup));

            services.RegisterServices();

            services.AddAutoMapperSetup();
        }

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

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }

        private void ConfigureContexts(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
              .AddDbContext<SmitUpContext>(options => options.UseNpgsql(Configuration.GetConnectionString("smitup")));

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<IdentityContext>(options => options.UseNpgsql(Configuration.GetConnectionString("smitup")));

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<EventStoreContext>(options => options.UseNpgsql(Configuration.GetConnectionString("smitup")));

            services.AddIdentityConfiguration(Configuration.GetSection("TokenConfigurations"));
        }
    }
}
