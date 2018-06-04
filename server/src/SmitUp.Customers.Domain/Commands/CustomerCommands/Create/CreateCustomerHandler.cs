using MediatR;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Commands;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Domain.Core.Transaction;
using SmitUp.Customers.Domain.Entities;
using SmitUp.Customers.Domain.Repositories;
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
            if (await VerifyUserExists(command))
                return await Task.FromResult<CreateCustomerResponse>(null);

            var user = new User(command.Username,command.Password);

            if (await VerifyCustomerExists(command))
                return await Task.FromResult<CreateCustomerResponse>(null);

            var customer = new Customer(user.Id,command.Name,command.Email,command.Gender,command.Birthday,command.MaritalStatus,user);

            await _repository.SaveCustomer(customer);

            if (await Commit())
            {
                var customerEvent = new CreateCustomerEvent(customer.Id,customer.User.Username, customer.Name, customer.Email,customer.Gender,customer.Birthday,customer.MaritalStatus);
                await _bus.RaiseEvent(customerEvent);

                return new CreateCustomerResponse(customer.Id, customer.User.Username, customer.Name, customer.Email, customer.Gender, customer.Birthday, customer.MaritalStatus);
            }

            return await Task.FromResult<CreateCustomerResponse>(null);
        }

        private async Task<bool> VerifyUserExists(CreateCustomerCommand command)
        {
            var exist = await _repository.UsernameExists(command.Username);
            if (exist)
            {
                await Notify("Username", "Username already exists");
            }

            return exist;
        }
        private async Task<bool> VerifyCustomerExists(CreateCustomerCommand command)
        {
            var exist = await _repository.EmailExists(command.Email);
            if (exist)
            {
                await Notify("Email", "Email already exists");
            }

            return exist;
        }
    }
}
