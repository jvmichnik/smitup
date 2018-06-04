namespace SmitUp.Customers.Domain.Commands.CustomerCommands.Create
{
    public class CreateCustomerValidation : CustomerCommandValidations<CreateCustomerCommand>
    {
        public CreateCustomerValidation()
        {
            ValidateUsername();
            ValidatePassword();
            ValidateName();
            ValidateEmail();
            ValidateGender();
            ValidateBirthday();
            ValidateMaritalStatus();
        }
    }
}
