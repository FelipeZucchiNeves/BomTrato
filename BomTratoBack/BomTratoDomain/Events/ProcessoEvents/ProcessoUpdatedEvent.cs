using BomTratoDomain.Entities;
using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Events.ProcessoEvents
{
    public class ProcessoUpdatedEvent : Event
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
        public ProcessoUpdatedEvent(Guid id, string processNumber, decimal value, Escritorio escritorio, Aprovador aprovador, Guid aprovadorId, Guid escritorioId, bool aproved, bool status, string complainerName)
        {
            Id = id;
            ProcessNumber = processNumber;
            Value = value;
            Escritorio = escritorio;
            Aprovador = aprovador;
            AprovadorId = aprovadorId;
            EscritorioId = escritorioId;
            Aproved = aproved;
            Status = status;
            ComplainerName = complainerName;
            AggregateId = id;
        }
    }
}
