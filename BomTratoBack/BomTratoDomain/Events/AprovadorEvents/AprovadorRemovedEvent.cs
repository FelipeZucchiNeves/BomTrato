using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Events.AprovadorEvents
{
    public class AprovadorRemovedEvent : Event
    {
        public Guid Id { get; set; }
        public AprovadorRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
