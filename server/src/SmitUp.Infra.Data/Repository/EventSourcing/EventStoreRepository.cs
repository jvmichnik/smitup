using Microsoft.EntityFrameworkCore;
using SmitUp.Domain.Core.Events;
using SmitUp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmitUp.Infra.Data.Repository.EventSourcing
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _context;
        private readonly DbSet<StoredEvent> db;

        public EventStoreRepository(EventStoreContext context)
        {
            _context = context;
            db = context.Set<StoredEvent>();
        }

        public async Task Store(StoredEvent theEvent)
        {
            db.Add(theEvent);
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
