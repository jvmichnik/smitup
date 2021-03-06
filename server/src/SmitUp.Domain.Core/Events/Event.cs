﻿using System;

namespace SmitUp.Domain.Core.Events
{
    public abstract class Event : Message
    {
        protected Event(Guid aggregateId)
        {
            Timestamp = DateTime.Now;
            AggregateId = aggregateId;
        }

        public DateTime Timestamp { get; private set; }
    }
}
