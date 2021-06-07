using BomTratoData.Context;
using BomTratoDomain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BomTratoData.Repository.EventSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;
        public EventStoreRepository(EventStoreContext context)
        {
            _context = context;
        }
        public async Task<IList<StoredEvent>> All(Guid aggregatedId)
        {
            return await(from e in _context.StoredEvent where e.AggregateId == aggregatedId select e).ToListAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        public void Store(StoredEvent theEvent)
        {
            _context.StoredEvent.Add(theEvent);
            _context.SaveChanges();
        }
    }
}
