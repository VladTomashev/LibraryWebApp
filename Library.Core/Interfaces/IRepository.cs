using Library.Core.Entities;

namespace Library.Core.Interfaces
{
    public interface IRepository<T> where T : AbstractEntity
    {
        public Task<IEnumerable<T>> GetAllAsync(PaginationParams paginationParams,
            CancellationToken cancellationToken = default);
        public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        public Task AddAsync(T entity, CancellationToken cancellationToken = default);
        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
