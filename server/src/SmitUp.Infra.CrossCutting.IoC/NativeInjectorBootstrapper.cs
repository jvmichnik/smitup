﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SmitUp.Customers.Domain.Commands.CustomerCommands.Create;
using SmitUp.Customers.Domain.Interfaces;
using SmitUp.Domain.Core.Behavior;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Interfaces;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Domain.Core.Transaction;
using SmitUp.Infra.CrossCutting.Bus;
using SmitUp.Infra.CrossCutting.Identity;
using SmitUp.Infra.Data.EventSourcing;
using SmitUp.Infra.Data.Repository;
using SmitUp.Infra.Data.Repository.EventSourcing;
using SmitUp.Infra.Data.Uow;

namespace SmitUp.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // ASP.NET HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //Domain - Commands
            RegisterCommands(services);

            // Domain - Events
            RegisterEvents(services);

            // Repositories
            RegisterRepositories(services);

            // Infra - Identity
            services.AddScoped<IUser, UserAuthenticated>();

            // Infra - Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - Bus
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandValidatorBehavior<,>));

            // Infra - Data EventSourcing
            RegisterEventStore(services);
        }

        private static void RegisterCommands(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>, CreateCustomerHandler>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {           
             services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        private static void RegisterEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }

        private static void RegisterEventStore(IServiceCollection services)
        {           
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, EventStore>();
        }
    }
}
