using BomTratoData.Repository.EventSourcing;
using BomTratoDomain.Entities;
using BomTratoDomain.Events.Interfaces;
using NetDevPack.Identity.User;
using NetDevPack.Messaging;
using Newtonsoft.Json;

namespace BomTratoData.EventSource
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IAspNetUser _user;
        public EventStore(IEventStoreRepository eventStoreRepository, IAspNetUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }
        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);
            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                _user.Name ?? _user.GetUserEmail());
            _eventStoreRepository.Store(storedEvent);
        }
    }
}
