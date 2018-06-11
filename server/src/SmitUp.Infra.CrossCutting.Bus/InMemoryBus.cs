using MediatR;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Commands;
using SmitUp.Domain.Core.Events;
using SmitUp.Domain.Core.Interfaces;
using System.Threading.Tasks;

namespace SmitUp.Infra.CrossCutting.Bus
{
    public class InMemoryBus : Bus, IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public InMemoryBus(IMediator mediator, IEventStore eventStore) : base(mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public async Task PublishCommand<T>(T command) where T : Command
        {
            await Publish(command);
        }

        public async Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> request)
        {
            return await Send(request);
        }

        public async Task RaiseEvent<T>(T @event) where T : Event
        {
            if (!@event.MessageType.Equals("DomainNotification"))
            {
                await _eventStore.Save(@event);
            }

            await Publish(@event);
        }

    }
}
