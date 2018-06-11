using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SmitUp.Infra.CrossCutting.Identity.Data;
using SmitUp.Infra.CrossCutting.Identity.Models;
using SmitUp.Infra.CrossCutting.Identity.Security.TokenConfig;

namespace SmitUp.Infra.CrossCutting.Identity.Security.Extensions
{
    public static class IdentityExtension
    {
        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration tokenParameters)
        {
            services.AddIdentity<User, Role>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<AccessManager>();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(tokenParameters)
                    .Configure(tokenConfigurations);

            services.AddSingleton(tokenConfigurations);

            services.AddJwtSecurity(signingConfigurations, tokenConfigurations);
        }
    }
}
