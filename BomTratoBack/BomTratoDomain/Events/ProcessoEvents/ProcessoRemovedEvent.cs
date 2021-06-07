using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Events.ProcessoEvents
{
    public class ProcessoRemovedEvent : Event
    {
        public Guid Id { get; set; }
        public ProcessoRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
