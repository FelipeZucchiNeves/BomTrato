using BomTratoDomain.Entities;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoDomain.Interfaces
{
    public interface IProcessoRepository : IRepository<Processo>
    {
        Task<Processo> GetById(Guid id);
        Task<Processo> GetByIdAprovador(Guid id);
        Task<Processo> GetByProcessNumber(string processNumber);
        Task<IEnumerable<Processo>> GetAll();
        void Add(Processo processo);
        void Update(Processo processo);
        void Remove(Processo processo);
    }
}
