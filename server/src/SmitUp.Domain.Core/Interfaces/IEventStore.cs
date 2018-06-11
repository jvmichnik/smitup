using SmitUp.Domain.Core.Events;
using System.Threading.Tasks;

namespace SmitUp.Domain.Core.Interfaces
{
    public interface IEventStore
    {
        Task Save<T>(T theEvent) where T : Event;
    }
}
