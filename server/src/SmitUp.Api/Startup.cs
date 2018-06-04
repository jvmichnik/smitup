using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var contextOptions = new DbContextOptionsBuilder()
            //    .UseNpgsql(Configuration.GetConnectionString("smitup"))
            //    .Options;

            //services
            //    .AddEntityFrameworkNpgsql()
            //    .AddSingleton(contextOptions)
            //.AddScoped<DbContext, SmitUpContext>();

            services.AddEntityFrameworkNpgsql()
               .AddDbContext<SmitUpContext>(options => options.UseNpgsql(Configuration.GetConnectionString("smitup")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR(typeof(Startup));
            services.RegisterServices();
   

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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
