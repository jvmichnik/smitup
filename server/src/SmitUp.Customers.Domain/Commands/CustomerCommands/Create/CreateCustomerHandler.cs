using MediatR;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Commands;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Domain.Core.Transaction;
using SmitUp.Customers.Domain.Entities;
using SmitUp.Customers.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmitUp.Customers.Domain.Commands.CustomerCommands.Create
{
    public class CreateCustomerHandler : CommandHandler, IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly IMediatorHandler _bus;
        private readonly ICustomerRepository _repository;

        public CreateCustomerHandler(IUnitOfWork uow, IMediatorHandler bus,INotificationHandler<DomainNotification> notifications, ICustomerRepository repository) 
            : base(uow, bus, notifications)
        {
            _bus = bus;
            _repository = repository;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customer = new Customer(command.Name, command.Gender, command.Birthday, command.MaritalStatus, command.UserId);

            await _repository.SaveCustomer(customer);

            if (await Commit())
            {
                var customerEvent = new CreateCustomerEvent(customer.Id,command.Username, customer.Name, command.Email,customer.Gender,customer.Birthday,customer.MaritalStatus);
                await _bus.RaiseEvent(customerEvent);

                return new CreateCustomerResponse(customer.Id, command.Username, customer.Name, command.Email, customer.Gender, customer.Birthday, customer.MaritalStatus);
            }

            return await Task.FromResult<CreateCustomerResponse>(null);
        }
    }
}
