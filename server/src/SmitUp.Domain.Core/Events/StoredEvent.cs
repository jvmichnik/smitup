using System;
using System.Collections.Generic;
using System.Text;

namespace SmitUp.Domain.Core.Events
{
    public class StoredEvent : Event
    {
        public StoredEvent(Event theEvent, string data, string user)
            :base(theEvent.AggregateId)
        {
            Id = Guid.NewGuid();
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        protected StoredEvent()
            :base(default(Guid))
        {

        }


        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}
