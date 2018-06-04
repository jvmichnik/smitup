using FluentValidation.Results;
using SmitUp.Domain.Core.Events;
using System;
using System.Threading.Tasks;

namespace SmitUp.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        public abstract Task<bool> IsValid();
    }
}
