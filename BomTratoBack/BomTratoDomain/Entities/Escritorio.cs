using NetDevPack.Domain;
using System;
using System.Collections.Generic;

namespace BomTratoDomain.Entities
{
    public class Escritorio : Entity, IAggregateRoot
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string State { get; set; }
        public int Cep { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public IEnumerable<Processo> Processos { get; set; }
        public Escritorio(Guid id, string street, string number, string state, int cep, string city, string district)
        {
            Id = id;
            Street = street;
            Number = number;
            State = state;
            Cep = cep;
            City = city;
            District = district;
        }
        public Escritorio(){ }
    }
}
