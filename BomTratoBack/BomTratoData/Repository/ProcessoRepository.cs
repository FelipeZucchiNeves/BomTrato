using BomTratoData.Context;
using BomTratoDomain.Entities;
using BomTratoDomain.Interfaces;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoData.Repository
{
    public class ProcessoRepository : IProcessoRepository
    {
        protected readonly BomtratoContext Db;
        protected readonly DbSet<Processo> DbSet;
        public ProcessoRepository(BomtratoContext db)
        {
            Db = db;
            DbSet = Db.Set<Processo>();
        }
        public IUnitOfWork UnitOfWork => Db;
        public void Add(Processo processo)
        {
            DbSet.Add(processo);
        }
        public void Dispose()
        {
            Db.Dispose();
        }
        public async Task<IEnumerable<Processo>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<Processo> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<Processo> GetByIdAprovador(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(a => a.AprovadorId == id);
        }
        public async Task<Processo> GetByProcessNumber(string processNumber)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(a => a.ProcessNumber == processNumber);
        }
        public void Remove(Processo processo)
        {
            DbSet.Remove(processo);
        }
        public void Update(Processo processo)
        {
            DbSet.Update(processo);
        }
    }
}
