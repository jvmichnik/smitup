using SmitUp.Domain.Core.Models;
using SmitUp.Customers.Domain.Enum;
using System;

namespace SmitUp.Customers.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(string name, string gender, DateTime birthday, EMaritalStatus maritalStatus, Guid userId)
        {
            Name = name;
            Gender = gender;
            Birthday = birthday;
            MaritalStatus = maritalStatus;
            UserId = userId;
        }
        protected Customer() { }

        public string Name { get; private set; }
        public string Gender { get; private set; }
        public DateTime Birthday { get; private set; }
        public EMaritalStatus MaritalStatus { get; private set; }
        public Guid UserId { get; private set; }
    }
}
