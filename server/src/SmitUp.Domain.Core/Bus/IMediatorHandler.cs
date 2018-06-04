using MediatR;
using SmitUp.Domain.Core.Commands;
using SmitUp.Domain.Core.Events;
using System.Threading.Tasks;

namespace SmitUp.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishCommand<T>(T command) where T : Command;
        Task<TResponse> SendCommand<TResponse>(IRequest<TResponse> command);

        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
