using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DataContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Book> availableBooks = await db.Books
                .AsNoTracking() 
                .Where(book => !db.BookRentals
                .Any(rental => rental.BookId == book.Id && !rental.IsReturned))
                .ToListAsync(cancellationToken);

            return availableBooks;
        }

        public async Task<IEnumerable<Book>> GetUnavailableBooksAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<Book> unavailableBooks = await db.Books
                .AsNoTracking()
                .Where(book => db.BookRentals
                .Any(rental => rental.BookId == book.Id && !rental.IsReturned))
                .ToListAsync(cancellationToken);

            return unavailableBooks;
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return await db.Books.Where(b => b.AuthorId == Id).ToListAsync(cancellationToken);
        }

        public async Task<Book> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
        {
            return await db.Books.Where(b => b.Isbn == isbn).FirstAsync(cancellationToken);
        }
    }
}
