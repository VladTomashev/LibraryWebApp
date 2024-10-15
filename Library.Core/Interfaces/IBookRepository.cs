using Library.Core.Entities;

namespace Library.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        public Task<Book> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
        public Task<IEnumerable<Book>> GetAvailableBooksAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Book>> GetUnavailableBooksAsync(CancellationToken cancellationToken = default);
        public Task<IEnumerable<Book>> GetByAuthorIdAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}
