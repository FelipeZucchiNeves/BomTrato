using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Events.EscritorioEvents
{
    public class EscritorioUpdatedEvent : Event
    {
        public Guid Id { get; protected set; }
        public string Street { get; protected set; }
        public string Number { get; protected set; }
        public string State { get; protected set; }
        public EscritorioUpdatedEvent(Guid id, string street, string number, string state)
        {
            Id = id;
            Street = street;
            Number = number;
            State = state;
            AggregateId = id;
        }
    }
}
