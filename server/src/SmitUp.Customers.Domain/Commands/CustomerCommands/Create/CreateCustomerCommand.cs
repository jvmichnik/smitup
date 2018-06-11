using MediatR;
using SmitUp.Customers.Domain.Enum;
using System;
using System.Threading.Tasks;

namespace SmitUp.Customers.Domain.Commands.CustomerCommands.Create
{
    public class CreateCustomerCommand : CustomerCommand, IRequest<CreateCustomerResponse>
    {
        public CreateCustomerCommand(string name, string gender, DateTime birthday, EMaritalStatus maritalStatus)
        {
            Name = name;
            Gender = gender;
            Birthday = birthday;
            MaritalStatus = maritalStatus;
        }

        public CreateCustomerCommand()
        {

        }

        public async override Task<bool> IsValid()
        {
            ValidationResult = await new CreateCustomerValidation().ValidateAsync(this);
            return ValidationResult.IsValid;
        }
    }
}
