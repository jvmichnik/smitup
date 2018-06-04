using System;

namespace SmitUp.Domain.Core.Events
{
    public abstract class Event : Message
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}
