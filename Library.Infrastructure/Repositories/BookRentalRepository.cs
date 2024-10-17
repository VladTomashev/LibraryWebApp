using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRentalRepository : Repository<BookRental>, IBookRentalRepository
    {
        public BookRentalRepository(DataContext db) : base(db)
        {
        }

        public async Task<IEnumerable<BookRental>> GetByUserIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await db.BookRentals.Where(br => br.UserProfileId == Id).ToListAsync(cancellationToken);
        }
    }
}
