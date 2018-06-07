using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmitUp.Customers.Domain.Commands.CustomerCommands.Create;
using SmitUp.Customers.Domain.Enum;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Infra.CrossCutting.Identity.Models;
using SmitUp.Infra.CrossCutting.Identity.Models.AccountViewModels;
using SmitUp.Infra.CrossCutting.Identity.Security;

namespace SmitUp.Api.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IMediatorHandler _bus;
        private readonly AccessManager _userManager;

        public AccountController(AccessManager userManager,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, bus)
        {
            _bus = bus;
            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel account)
        {
            if (!ModelState.IsValid)
                NotifyModelStateErrors();

            var user = new User(account.UserName, account.Email, account.Password);

            var result = await _userManager.ValidateUser(user);

            if (result.Succeeded)
            {
                var command = new CreateCustomerCommand("teste", "123123123", "testeteste", "teste@teste.com", "M", new DateTime(2016, 10, 10), EMaritalStatus.Single, user.Id);
                var response = await _bus.SendCommand(command);

                return Response(response);
            }

            await AddIdentityErrors(result);
            return ResponseBadRequest();
        }
    }
}