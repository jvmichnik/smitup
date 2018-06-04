using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmitUp.Customers.Domain.Commands.CustomerCommands.Create;
using SmitUp.Customers.Domain.Enum;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;

namespace SmitUp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseController
    {
        private readonly IMediatorHandler _bus;
        public ValuesController(IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
            :base(notifications)
        {
            _bus = bus;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var command = new CreateCustomerCommand("teste","123123123","testeteste","teste@teste.com","M",new DateTime(2016,10,10),EMaritalStatus.Single);

            try
            {
                await _bus.SendCommand(command);
            }
            catch (Exception e)
            {

                throw;
            }

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
