using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmitUp.Customers.Domain.Commands.CustomerCommands.Create;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;

namespace SmitUp.Api.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {

        private readonly IMediatorHandler _bus;

        public CustomerController(
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, bus)
        {
            _bus = bus;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("customer/register")]
        public async Task<IActionResult> Register([FromBody]CreateCustomerCommand command)
        {
            var response = await _bus.SendCommand(command);
            return Response(response);
        }
    }
}