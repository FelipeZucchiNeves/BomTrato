using BomTratoDomain.Entities;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoDomain.Interfaces
{
    public interface IAprovadorRepository : IRepository<Aprovador>
    {
        Task<Aprovador> GetByEmail(string email);
        Task<Aprovador> GetById(Guid id);
        Task<IEnumerable<Aprovador>> GetAll();
        void Add(Aprovador aprovador);
        void Update(Aprovador aprovador);
        void Remove(Aprovador aprovador);
    }
}
