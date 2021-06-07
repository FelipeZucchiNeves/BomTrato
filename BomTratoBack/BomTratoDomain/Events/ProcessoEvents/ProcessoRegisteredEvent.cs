using BomTratoDomain.Entities;
using NetDevPack.Messaging;
using System;
namespace BomTratoDomain.Events.ProcessoEvents
{
    public class ProcessoRegisteredEvent : Event
    {
        public Guid Id { get; set; }
        public string ProcessNumber { get; set; }
        public decimal Value { get; set; }
        public Guid AprovadorId { get; set; }
        public Guid EscritorioId { get; set; }
        public string ComplainerName { get; set; }
        public bool Aproved { get; set; }
        public bool Status { get; set; }
        public Escritorio Escritorio { get; set; }
        public Aprovador Aprovador { get; set; }
        public ProcessoRegisteredEvent(Guid id, string idProcessNumber, decimal value, Guid aprovadorId, Guid escritorioId, bool aproved, bool status, string complainerName)
        {
            Id = id;
            ProcessNumber = idProcessNumber;
            Value = value;
            AprovadorId = aprovadorId;
            EscritorioId = escritorioId;
            ComplainerName = complainerName;
            Aproved = aproved;
            Status = status;
            AggregateId = id;
        }
    }
}
