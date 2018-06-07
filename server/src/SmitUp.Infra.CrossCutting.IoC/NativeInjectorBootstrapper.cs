using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmitUp.Customers.Domain.Commands.CustomerCommands.Create;
using SmitUp.Customers.Domain.Interfaces;
using SmitUp.Domain.Core.Behavior;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Domain.Core.Transaction;
using SmitUp.Infra.CrossCutting.Bus;
using SmitUp.Infra.Data.Repository;
using SmitUp.Infra.Data.Uow;

namespace SmitUp.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandValidatorBehavior<,>));

            // Domain - Events
            RegisterEvents(services);

            //Domain - Commands
            RegisterCommands(services);

            // Repositories
            RegisterRepositories(services);

            // Unit of Work
            RegisterUow(services);
        }

        private static void RegisterCommands(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>, CreateCustomerHandler>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {           
             services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        private static void RegisterUow(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void RegisterEvents(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}
