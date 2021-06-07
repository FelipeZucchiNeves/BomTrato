using BomTratoDomain.Entities;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoDomain.Interfaces
{
    public interface IEscritorioRepository : IRepository<Escritorio>
    {
        Task<Escritorio> GetById(Guid id);
        Task<IEnumerable<Escritorio>> GetAll();
        void Add(Escritorio aprovador);
        void Update(Escritorio aprovador);
        void Remove(Escritorio aprovador);
    }
}
