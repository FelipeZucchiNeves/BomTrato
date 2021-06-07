using BomTratoData.Mapping;
using BomTratoDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BomTratoData.Context
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options) { }
        public DbSet<StoredEvent> StoredEvent { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
