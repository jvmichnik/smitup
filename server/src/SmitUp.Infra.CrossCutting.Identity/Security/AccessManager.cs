using MediatR;
using Microsoft.AspNetCore.Identity;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Infra.CrossCutting.Identity.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmitUp.Infra.CrossCutting.Identity.Security
{
    public class AccessManager
    {
        private readonly UserManager<User> _userManager;
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        public AccessManager(
                UserManager<User> userManager,
                INotificationHandler<DomainNotification> notifications,
                IMediatorHandler mediator
            )
        {
            _userManager = userManager;
            _mediator = mediator;
            _notifications = (DomainNotificationHandler)notifications;
        }
        public async Task<IdentityResult> CreateUser(User user)
        {
            return await _userManager.CreateAsync(user, user.PasswordHash);
        }

        public async Task<IdentityResult> ValidateUser(User user)
        {
            var userValidator = new UserValidator<User>();
            return await userValidator.ValidateAsync(_userManager,user);
        }
    }
}
