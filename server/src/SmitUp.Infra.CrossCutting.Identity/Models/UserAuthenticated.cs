﻿using Microsoft.AspNetCore.Http;
using SmitUp.Domain.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmitUp.Infra.CrossCutting.Identity.Models
{
    public class UserAuthenticated : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAuthenticated(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
