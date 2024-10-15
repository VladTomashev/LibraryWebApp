using Library.Core.Entities;

namespace Library.Core.Interfaces
{
    public interface IBookRentalRepository : IRepository<BookRental>
    {
        public Task<IEnumerable<BookRental>> GetByUserIdAsync(Guid Id, CancellationToken cancellationToken = default);
    }
}
