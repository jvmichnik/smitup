using SmitUp.Customers.Domain.Enum;
using System;

namespace SmitUp.Customers.Domain.Commands.CustomerCommands.Create
{
    public class CreateCustomerResponse
    {
        public CreateCustomerResponse(Guid id, string username, string name, string email, string gender, DateTime birthday, EMaritalStatus maritalStatus)
        {
            Id = id;
            Username = username;
            Name = name;
            Email = email;
            Gender = gender;
            Birthday = birthday;
            MaritalStatus = maritalStatus;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Gender { get; private set; }
        public DateTime Birthday { get; private set; }
        public EMaritalStatus MaritalStatus { get; private set; }
    }
}
