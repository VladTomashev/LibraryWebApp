using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : AbstractEntity
    {
        protected DataContext db;

        public Repository(DataContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await db.Set<T>().AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            db.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(PaginationParams? paginationParams, 
            CancellationToken cancellationToken = default)
        {
            if (paginationParams == null)
            {
                return await db.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
            }
            else
            {
                return await db.Set<T>().AsNoTracking()
                    .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                    .Take(paginationParams.PageSize)
                    .ToListAsync(cancellationToken);
            }
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await db.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            db.Set<T>().Update(entity);
        }
    }
}
