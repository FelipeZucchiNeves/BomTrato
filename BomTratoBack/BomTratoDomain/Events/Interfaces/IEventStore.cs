using NetDevPack.Messaging;

namespace BomTratoDomain.Events.Interfaces
{
    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
