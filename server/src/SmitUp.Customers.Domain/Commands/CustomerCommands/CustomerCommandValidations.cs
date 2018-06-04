using FluentValidation;
using System;

namespace SmitUp.Customers.Domain.Commands.CustomerCommands
{
    public class CustomerCommandValidations<T> : AbstractValidator<T> where T : CustomerCommand
    {
        private const int MINIMUM_USERNAME_LENGTH = 3;
        private const int MAXIMUM_USERNAME_LENGTH = 30;
        private const string USERNAME_REQUIRED_MSG = "Username is required.";
        private string USERNAME_LENGTH_MSG = $"Username must be between {MINIMUM_USERNAME_LENGTH} and {MAXIMUM_USERNAME_LENGTH} characters.";

        private const int MINIMUM_PASSWORD_LENGTH = 8;
        private const string PASSWORD_REQUIRED_MSG = "Password is required.";
        private string PASSWORD_LENGTH_MSG = $"Password must be at least {MINIMUM_PASSWORD_LENGTH} characters.";

        private const int MINIMUM_NAME_LENGTH = 3;
        private const int MAXIMUM_NAME_LENGTH = 200;
        private const string NAME_REQUIRED_MSG = "Name is required.";
        private string NAME_LENGTH_MSG = $"Name must be between {MINIMUM_NAME_LENGTH} and {MAXIMUM_USERNAME_LENGTH} characters.";

        private const string EMAIL_INVALID_MSG = "Email is not valid.";

        private const string GENDER_REQUIRED_MSG = "Gender is required.";
        private const string GENDER_INVALID_MSG = "Gender is invalid.";

        private const string BIRTHDAY_REQUIRED_MSG = "Birthday is required.";
        private const string BIRTHDAY_INVALID_MSG = "Birthday is not valid.";

        private const string MARITAL_REQUIRED_MSG = "Marital Status is required.";
        private const string MARITAL_INVALID_MSG = "Marital Status is not valid.";

        protected void ValidateUsername()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(USERNAME_REQUIRED_MSG);

            RuleFor(x => x.Username)
                .Length(MINIMUM_USERNAME_LENGTH, MAXIMUM_USERNAME_LENGTH)
                .WithMessage(USERNAME_LENGTH_MSG);
        }

        protected void ValidatePassword()
        {
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(PASSWORD_REQUIRED_MSG);

            RuleFor(x => x.Password)
                .MinimumLength(MINIMUM_PASSWORD_LENGTH)
                .WithMessage(PASSWORD_LENGTH_MSG);
        }

        protected void ValidateName()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(NAME_REQUIRED_MSG)
                .Length(MINIMUM_NAME_LENGTH, MAXIMUM_NAME_LENGTH)
                .WithMessage(NAME_LENGTH_MSG);
        }

        protected void ValidateEmail()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(EMAIL_INVALID_MSG);
        }

        protected void ValidateGender()
        {
            RuleFor(x => x.Gender)
                .NotEmpty()
                .WithMessage(GENDER_REQUIRED_MSG);

            RuleFor(x => x.Gender)
                .Custom((v, context) =>
                {
                    if (!(v.ToUpper().Equals('M') || v.ToUpper().Equals('F')))
                        context.AddFailure(GENDER_INVALID_MSG);
                });
        }

        protected void ValidateBirthday()
        {
            RuleFor(x => x.Birthday)
                .NotEmpty().WithMessage(BIRTHDAY_REQUIRED_MSG);

            RuleFor(x => x.Birthday)
                .LessThan(p => DateTime.Now).GreaterThan(p => DateTime.Now.AddYears(-120)).WithMessage(BIRTHDAY_INVALID_MSG);
        }

        protected void ValidateMaritalStatus()
        {
            RuleFor(x => x.MaritalStatus)
                .NotEmpty().WithMessage(MARITAL_REQUIRED_MSG);

            RuleFor(x => x.MaritalStatus)
                .IsInEnum().WithMessage(MARITAL_INVALID_MSG);
        }
    }
}
