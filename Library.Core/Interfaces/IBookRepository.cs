using Library.Core.Entities;

namespace Library.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default);
        Task<List<Book>> GetAvailableBooksAsync(CancellationToken cancellationToken = default);
        Task<List<Book>> GetUnavailableBooksAsync(CancellationToken cancellationToken = default);
        Task<List<Book>> GetByAuthorIdAsync(CancellationToken cancellationToken = default);
    }
}
