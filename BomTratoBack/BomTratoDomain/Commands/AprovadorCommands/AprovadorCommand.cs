using BomTratoDomain.Entities;
using NetDevPack.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BomTratoDomain.Commands.AprovadorCommands
{
    public class AprovadorCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public DateTime BirthDate { get; protected set; }
        public IEnumerable<Processo> Processos { get; set; }
    }
}
