using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Commands.ProcessoCommands
{
    public class ProcessoCommand : Command
    {
        public Guid Id { get; set; }
        public string ProcessNumber { get; set; }
        public decimal Value { get; set; }
        public Guid AprovadorId { get; set; }
        public Guid EscritorioId { get; set; }
        public bool Aproved { get; set; }
        public bool Status { get; set; }
        public string ComplainerName { get; set; }
    }
}

