using MediatR;
using SmitUp.Customers.Domain.Enum;
using System;
using System.Threading.Tasks;

namespace SmitUp.Customers.Domain.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommand : CustomerCommand, IRequest<CreateCustomerResponse>
    {
        public CreateCustomerCommand(string username, string password, string name, string email, string gender, DateTime birthday, EMaritalStatus maritalStatus)
        {
            Username = username;
            Password = password;
            Name = name;
            Email = email;
            Gender = gender;
            Birthday = birthday;
            MaritalStatus = maritalStatus;
        }

        public async override Task<bool> IsValid()
        {
            ValidationResult = await new CreateCustomerValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
