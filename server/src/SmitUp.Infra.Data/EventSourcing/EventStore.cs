using Newtonsoft.Json;
using SmitUp.Domain.Core.Events;
using SmitUp.Domain.Core.Interfaces;
using SmitUp.Infra.Data.Repository.EventSourcing;
using System.Threading.Tasks;

namespace SmitUp.Infra.Data.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public EventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }

        public async Task Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name);

            await _eventStoreRepository.Store(storedEvent);
        }
    }
}
