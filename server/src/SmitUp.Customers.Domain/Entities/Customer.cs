using SmitUp.Domain.Core.Models;
using SmitUp.Customers.Domain.Enum;
using System;

namespace SmitUp.Customers.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(string name, string email, string gender, DateTime birthday, EMaritalStatus maritalStatus, User user)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Gender = gender;
            Birthday = birthday;
            MaritalStatus = maritalStatus;
            UserId = user.Id;
            User = user;
        }
        protected Customer() { }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Gender { get; private set; }
        public DateTime Birthday { get; private set; }
        public EMaritalStatus MaritalStatus { get; private set; }
        public Guid UserId { get; private set; }

        public virtual User User { get; private set; }
    }
}
