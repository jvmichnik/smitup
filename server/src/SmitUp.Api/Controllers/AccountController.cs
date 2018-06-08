using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly AccessManager _accessManager;

        public AccountController(
            AccessManager accessManager,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, bus)
        {
            _bus = bus;
            _accessManager = accessManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel account)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ResponseBadRequest();
            }

            var user = new User(account.UserName, account.Email, account.Password);

            var result = await _accessManager.CreateUser(user);

            if (result.Succeeded)
            {
                var token = await _accessManager.GenerateToken(user);
                return Response(token);
            }

            await AddIdentityErrors(result);
            return ResponseBadRequest();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/token")]
        public async Task<IActionResult> GetToken([FromBody]LoginViewModel account)
        {
            var user = new User(account.Username, account.Password);

            var token = await _accessManager.ValidateCredentials(user);

            if (token != null)
            {
                return Response(token);
            }

            return Response("Failed to authenticate");
        }

        [HttpPost]
        [Route("account/send_verification_email")]
        public async Task<IActionResult> SendVerificationEmail()
        {
            var userId = HttpContext.User.Identity.Name;
            var user = await _accessManager.GetUser(userId);
            if (user == null)
            {
                NotifyError("UserID", $"Unable to load user");
            }

            var emailToken = await _accessManager.GenerateEmailConfirmationToken(user);

            return Response(new
            {
                EmailToken = emailToken
            });
        }

        [HttpPost]
        [Route("account/confirm_email")]
        public async Task<IActionResult> ConfirmEmail(string code)
        {
            if (code == null)
            {
                return BadRequest();
            }
            var userId = HttpContext.User.Identity.Name;
            var user = await _accessManager.GetUser(userId);
            if (user == null)
            {
                NotifyError("UserID", $"Unable to load user");
            }
            var result = await _accessManager.ConfirmEmailToken(user, code);

            if (result.Succeeded)
                return Response($"Email successfully confirmed");

            await AddIdentityErrors(result);
            return ResponseBadRequest();
        }
    }
}