using NetDevPack.Domain;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BomTratoDomain.Entities
{
    public class Processo : Entity, IAggregateRoot
    {
        public string ProcessNumber { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal Value { get; set; }
        public Guid AprovadorId { get; set; }
        public Guid EscritorioId { get; set; }
        public bool Aproved { get; set; }
        public bool Status { get; set; }
        public string ComplainerName { get; set; }
        public Aprovador Aprovador { get; set; }
        public Escritorio Escritorio { get; set; }
        public Processo(Guid id, string processNumber, decimal value, Guid aprovadorId, Guid escritorioId, bool aproved, bool status, string complainerName)
        {
            Id = id;
            ProcessNumber = processNumber;
            Value = value;
            EscritorioId = escritorioId;
            AprovadorId = aprovadorId;
            Aproved = aproved;
            Status = status;
            ComplainerName = complainerName;
        }
        public Processo(){ }
    }
}
