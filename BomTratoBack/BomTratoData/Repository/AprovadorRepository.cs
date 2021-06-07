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
    public class AprovadorRepository : IAprovadorRepository
    {
        protected readonly BomtratoContext Db;
        protected readonly DbSet<Aprovador> DbSet;
        public AprovadorRepository(BomtratoContext db)
        {
            Db = db;
            DbSet = Db.Set<Aprovador>();
        }
        public IUnitOfWork UnitOfWork => Db;
        public void Add(Aprovador aprovador)
        {
            DbSet.Add(aprovador);
        }
        public void Remove(Aprovador aprovador)
        {
            DbSet.Remove(aprovador);
        }
        public void Update(Aprovador aprovador)
        {
            DbSet.Update(aprovador);
        }
        public void Dispose()
        {
            Db.Dispose();
        }
        public async Task<IEnumerable<Aprovador>> GetAll()
        {
            return await DbSet.ToListAsync();
        }
        public async Task<Aprovador> GetByEmail(string email)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task<Aprovador> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
