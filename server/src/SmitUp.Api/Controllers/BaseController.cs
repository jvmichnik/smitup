using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmitUp.Api.Controllers
{
    public class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        public BaseController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        private IActionResult ResponseOk(object result)
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        protected IActionResult ResponseBadRequest()
        {
            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications()
            });
        }

        protected new IActionResult Response(object result)
        {
            return IsValidOperation() ? ResponseOk(result) : ResponseBadRequest();
        }

        protected bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected Task AddIdentityErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(error.Code, error.Description);
            }

            return Task.CompletedTask;
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.PublishNotification(new DomainNotification(code, message));
        }

    }
}
