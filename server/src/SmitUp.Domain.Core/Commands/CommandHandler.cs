using MediatR;
using SmitUp.Domain.Core.Bus;
using SmitUp.Domain.Core.Notifications;
using SmitUp.Domain.Core.Transaction;
using System.Threading.Tasks;

namespace SmitUp.Domain.Core.Commands
{
    public abstract class CommandHandler
    {
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;
        private readonly IUnitOfWork _uow;

        protected CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }

        protected virtual async Task NotifyValidationErrors(Command message)
        {
            foreach (var error in message.ValidationResult.Errors)
            {
                await Notify(error.PropertyName, error.ErrorMessage);
            }
        }

        protected virtual Task Notify(string key, string value)
        {
            return Notify(new DomainNotification(key, value));
        }

        protected virtual Task Notify(DomainNotification notification)
        {
            return _bus.PublishNotification(notification);
        }

        protected virtual bool IsValid()
        {
            return !_notifications.HasNotifications();
        }

        protected virtual async Task<CommandResponse> Commit()
        {
            if (!IsValid())
            {
                return CommandResponse.Fail;
            }

            var commandResponse = await _uow.Commit();

            if (!commandResponse)
            {
                await Notify("Commit", "We had a problem during saving your data.");
            }

            return commandResponse;
        }
    }
}
