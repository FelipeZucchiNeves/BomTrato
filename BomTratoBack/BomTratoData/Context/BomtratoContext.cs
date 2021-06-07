using BomTratoData.Mapping;
using BomTratoDomain.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System.Linq;
using System.Threading.Tasks;

namespace BomTratoData.Context
{
    public class BomtratoContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        public BomtratoContext(DbContextOptions<BomtratoContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
        public DbSet<Aprovador> Aprovador { get; set; }
        public DbSet<Escritorio> Escritorios { get; set; }
        public DbSet<Processo> Processos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfiguration(new AprovadorMap());

            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> Commit()
        {
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
            var success = await SaveChangesAsync() > 0;
            return success;
        }
    }
}
