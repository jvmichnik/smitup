namespace SmitUp.Customers.Domain.Commands.CustomerCommands.Create
{
    public class CreateCustomerValidation : CustomerCommandValidations<CreateCustomerCommand>
    {
        public CreateCustomerValidation()
        {
            ValidateName();
            ValidateGender();
            ValidateBirthday();
            ValidateMaritalStatus();
        }
    }
}
