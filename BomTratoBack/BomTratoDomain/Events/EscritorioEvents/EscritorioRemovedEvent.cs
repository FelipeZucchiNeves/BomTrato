using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Events.EscritorioEvents
{
    public class EscritorioRemovedEvent : Event
    {
        public Guid Id { get; set; }
        public EscritorioRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
