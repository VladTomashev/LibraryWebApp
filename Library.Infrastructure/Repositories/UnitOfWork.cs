using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;

namespace Library.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        private IAuthorRepository _authorRepository;
        private IBookRentalRepository _bookRentalRepository;
        private IBookRepository _bookRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IUserAuthRepository _userAuthRepository;
        private IUserProfileRepository _userProfileRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IAuthorRepository AuthorRepository =>
            _authorRepository ??= new AuthorRepository(_context);

        public IBookRentalRepository BookRentalRepository =>
            _bookRentalRepository ??= new BookRentalRepository(_context);

        public IBookRepository BookRepository =>
            _bookRepository ??= new BookRepository(_context);

        public IRefreshTokenRepository RefreshTokenRepository =>
            _refreshTokenRepository ??= new RefreshTokenRepository(_context);

        public IUserAuthRepository UserAuthRepository =>
            _userAuthRepository ??= new UserAuthRepository(_context);

        public IUserProfileRepository UserProfileRepository =>
            _userProfileRepository ??= new UserProfileRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
