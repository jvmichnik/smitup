using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmitUp.Api.ViewModels.Customer;
using SmitUp.Customers.Domain.Commands.CustomerCommands.Create;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;

namespace SmitUp.Api.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;

        public CustomerController(
            IMapper mapper,
            IMediatorHandler bus,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, bus)
        {
            _mapper = mapper;
            _bus = bus;
        }

        [HttpPost]
        [Route("customer/register")]
        public async Task<IActionResult> Register([FromBody]CreateNewCustomerViewModel createViewModel)
        {
            var command = _mapper.Map<CreateCustomerCommand>(createViewModel);

            var response = await _bus.SendCommand(command);
            return Response(response);
        }
    }
}