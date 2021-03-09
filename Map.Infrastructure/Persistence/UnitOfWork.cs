using System.Threading.Tasks;
using Map.Core.Interfaces;
using Map.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Map.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MapDbContext _context;
        public UnitOfWork(MapDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public virtual void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public virtual void RollBackTransaction()
        {
            _context.Database.RollbackTransaction();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
