using MediatR;
using System;

namespace SmitUp.Domain.Core.Events
{
    public abstract class Message : INotification
    {
        protected Message()
        {
            MessageType = GetType().Name;
        }

        public string MessageType { get; protected set; }

        public Guid AggregateId { get; protected set; }
    }
}
