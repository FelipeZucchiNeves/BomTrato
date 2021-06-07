using NetDevPack.Messaging;
using System;

namespace BomTratoDomain.Commands.EscritorioCommands
{
    public class EscritorioCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Street { get; protected set; }
        public string Number { get; protected set; }
        public string State { get; protected set; }
        public int Cep { get; set; }
        public string City { get; set; }
        public string District { get; set; }
    }
}
