using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SmitUp.Api.AutoMapper.Account;
using SmitUp.Api.AutoMapper.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmitUp.Api.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper();

            RegisterMappings();
        }

        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AccountViewModelToDomainMappingProfile());

                cfg.AddProfile(new CustomerViewModelToDomainMappingProfile());
            });
        }
    }
}
