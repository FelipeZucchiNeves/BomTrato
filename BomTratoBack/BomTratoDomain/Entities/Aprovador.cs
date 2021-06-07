using NetDevPack.Domain;
using System;
using System.Collections.Generic;

namespace BomTratoDomain.Entities
{
    public class Aprovador : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public IEnumerable<Processo> Processos { get; set; }
        public Aprovador(){ }
        public Aprovador(Guid id, string name, string lastName, string email, DateTime birthDate)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }
    }
}
