using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public BaseController(INotificationHandler<DomainNotification> notifications)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        public bool IsValidOperation()
        {
            return !_notifications.HasNotifications();
        }

        private IActionResult ResponseOk(object result)
        {
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        public IActionResult ResponseBadRequest()
        {
            return BadRequest(new
            {
                success = false,
                errors = _notifications.GetNotifications()
            });
        }

        public new IActionResult Response(object result)
        {
            return IsValidOperation() ? ResponseOk(result) : ResponseBadRequest();
        }
    }
}
