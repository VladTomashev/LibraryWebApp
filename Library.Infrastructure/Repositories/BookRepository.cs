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

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync(PaginationParams paginationParams,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Book> availableBooks = await db.Books
                .AsNoTracking() 
                .Where(book => !db.BookRentals
                .Any(rental => rental.BookId == book.Id && !rental.IsReturned))
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync(cancellationToken);

            return availableBooks;
        }

        public async Task<IEnumerable<Book>> GetUnavailableBooksAsync(PaginationParams paginationParams,
            CancellationToken cancellationToken = default)
        {
            IEnumerable<Book> unavailableBooks = await db.Books
                .AsNoTracking()
                .Where(book => db.BookRentals
                .Any(rental => rental.BookId == book.Id && !rental.IsReturned))
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync(cancellationToken);

            return unavailableBooks;
        }

        public async Task<IEnumerable<Book>> GetByAuthorIdAsync(Guid Id, PaginationParams paginationParams,
            CancellationToken cancellationToken = default)
        {
            return await db.Books.Where(b => b.AuthorId == Id)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<Book> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
        {
            return await db.Books.Where(b => b.Isbn == isbn).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
