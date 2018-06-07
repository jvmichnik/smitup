using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmitUp.Infra.CrossCutting.Identity.Data;
using SmitUp.Infra.CrossCutting.Identity.Models;
using SmitUp.Infra.CrossCutting.Identity.Security;
using SmitUp.Infra.CrossCutting.IoC;
using SmitUp.Infra.Data.Context;
using System;

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

            ConfigureIdentity(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors();

            services.AddMediatR(typeof(Startup));
            services.RegisterServices();


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

        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddEntityFrameworkNpgsql()
              .AddDbContext<SmitUpContext>(options => options.UseNpgsql(Configuration.GetConnectionString("smitup")));

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<SmitUpIdentityDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("smitup")));

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SmitUpIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<AccessManager>();
        }
    }
}
