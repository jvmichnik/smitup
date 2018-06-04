using MediatR;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Commands;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Domain.Core.Transaction;
using System.Threading;
using System.Threading.Tasks;

namespace SmitUp.Domain.Core.Behavior
{
    public class CommandValidatorBehavior<TRequest, TResponse> : CommandHandler, IPipelineBehavior<TRequest, TResponse>
    {
        public CommandValidatorBehavior(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
            : base(uow, bus, notifications)
        {
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if ((request is Command))
            {
                var command = request as Command;
                if (!await command.IsValid())
                {
                    await NotifyValidationErrors(command);
                    return default(TResponse);
                }
            }

            return await next();
        }
    }
}
