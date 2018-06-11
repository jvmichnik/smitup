using Microsoft.AspNetCore.Http;
using SmitUp.Domain.Core.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace SmitUp.Infra.CrossCutting.Identity
{
    public class UserAuthenticated : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAuthenticated(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "username").Value;

        public Guid Id => Guid.Parse(_accessor.HttpContext.User.Identity.Name);

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
