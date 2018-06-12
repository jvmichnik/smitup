using System;
using System.Collections.Generic;
using System.Text;

namespace SmitUp.Domain.Core.Events
{
    public class Notification : Message
    {
        protected Notification()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}
