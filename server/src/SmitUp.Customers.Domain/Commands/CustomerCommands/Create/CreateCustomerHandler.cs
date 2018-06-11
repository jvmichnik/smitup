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
using SmitUp.Domain.Core.Interfaces;

namespace SmitUp.Customers.Domain.Commands.CustomerCommands.Create
{
    public class CreateCustomerHandler : CommandHandler, IRequestHandler<CreateCustomerCommand, CreateCustomerResponse>
    {
        private readonly IMediatorHandler _bus;
        private readonly ICustomerRepository _repository;
        private readonly IUser _user;

        public CreateCustomerHandler(IUnitOfWork uow, IMediatorHandler bus,INotificationHandler<DomainNotification> notifications, ICustomerRepository repository, IUser user) 
            : base(uow, bus, notifications)
        {
            _bus = bus;
            _repository = repository;
            _user = user;
        }

        public async Task<CreateCustomerResponse> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var user = _user.Id;

            if (await VerifyCustomerAlreadyCreatedToUser(user))
                return await Task.FromResult<CreateCustomerResponse>(null);

            var customer = new Customer(command.Name, command.Gender, command.Birthday, command.MaritalStatus, user);

            await _repository.SaveCustomer(customer);

            if (await Commit())
            {
                var customerEvent = new CreateCustomerEvent(customer.Id, customer.Name,customer.Gender,customer.Birthday,customer.MaritalStatus);
                await _bus.RaiseEvent(customerEvent);

                return new CreateCustomerResponse(customer.Id, customer.Name, customer.Gender, customer.Birthday, customer.MaritalStatus);
            }

            return await Task.FromResult<CreateCustomerResponse>(null);
        }

        private async Task<bool> VerifyCustomerAlreadyCreatedToUser(Guid userId)
        {
            var customer = await _repository.GetCustomerByUser(userId);

            if (customer != null)
            {
                await Notify("Customer", "Customer already exists for this user.");
                return true;
            }

            return false;
        }
    }
}
