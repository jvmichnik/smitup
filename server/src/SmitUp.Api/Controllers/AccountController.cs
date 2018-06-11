using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmitUp.Api.Account.ViewModels;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Infra.CrossCutting.Identity.Models;
using SmitUp.Infra.CrossCutting.Identity.Security;

namespace SmitUp.Api.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IMediatorHandler _bus;
        private readonly AccessManager _accessManager;
        private readonly IMapper _mapper;

        public AccountController(
            IMapper mapper,
            AccessManager accessManager,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, bus)
        {
            _mapper = mapper;
            _bus = bus;
            _accessManager = accessManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("account/register")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ResponseBadRequest();
            }

            var user = _mapper.Map<User>(registerViewModel);

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
        public async Task<IActionResult> GetToken([FromBody]LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return ResponseBadRequest();
            }

            var user = _mapper.Map<User>(loginViewModel);

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
                NotifyError("user", $"unable to load user");
                return ResponseBadRequest();
            }

            var emailToken = await _accessManager.GenerateEmailConfirmationToken(user);

            return Response(new
            {
                EmailConfirmCode = emailToken
            });
        }

        [HttpPost]
        [Route("account/confirm_email")]
        public async Task<IActionResult> ConfirmEmail(string code)
        {
            if (code == null)
            {
                NotifyError("code", $"code value is required");
                return ResponseBadRequest();
            }

            var userId = HttpContext.User.Identity.Name;
            var user = await _accessManager.GetUser(userId);
            if (user == null)
            {
                NotifyError("user", $"Unable to load user");
                return ResponseBadRequest();
            }

            var result = await _accessManager.ConfirmEmailToken(user, code);

            if (result.Succeeded)
                return Response($"Email successfully confirmed");

            await AddIdentityErrors(result);
            return ResponseBadRequest();
        }
    }
}