using SmitUp.Domain.Core.Models;
using SmitUp.Customers.Domain.Cryptography;
using System;

namespace SmitUp.Customers.Domain.Entities
{
    public class User : Entity
    {
        public User(string username, string password)
        {
            Username = username;
            Password = Encrypt(password);
        }
        protected User() { }

        public string Username { get; private set; }
        public string Password { get; private set; }

        public Customer Customer { get; protected set; }

        public bool Authenticate(string username, string password)
        {
            var passwordHash = new PasswordHash();
            return Username == username && passwordHash.ValidatePassword(password, Password);
        }

        private string Encrypt(string senha)
        {
            return new PasswordHash().CreateHash(senha);
        }
    }
}
