using BomTratoDomain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BomTratoData.Repository.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregatedId);
    }
}
