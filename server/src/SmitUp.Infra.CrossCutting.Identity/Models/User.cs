using Microsoft.AspNetCore.Identity;
using System;

namespace SmitUp.Infra.CrossCutting.Identity.Models
{
    public class User : IdentityUser<Guid>
    {
        public User(string userName, string email, string password)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            Email = email;
            PasswordHash = password;
        }
        public User()
        {
                
        }
    }
}
