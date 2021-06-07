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
    public class EscritorioRepository : IEscritorioRepository
    {
        protected readonly BomtratoContext Db;
        protected readonly DbSet<Escritorio> DbSet;

        public EscritorioRepository(BomtratoContext db)
        {
            Db = db;
            DbSet = Db.Set<Escritorio>();
        }
        public IUnitOfWork UnitOfWork => Db;
        public void Add(Escritorio escritorio)
        {
            DbSet.Add(escritorio);
        }
        public void Remove(Escritorio escritorio)
        {
            DbSet.Remove(escritorio);
        }
        public void Update(Escritorio escritorio)
        {
            DbSet.Update(escritorio);
        }
        public void Dispose()
        {
            Db.Dispose();
        }
        public async Task<IEnumerable<Escritorio>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<Escritorio> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
