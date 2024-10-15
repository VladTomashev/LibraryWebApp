using Library.Core.Interfaces;

namespace Library.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IAuthorRepository AuthorRepository { get; }
        public IBookRentalRepository BookRentalRepository { get; }
        public IBookRepository BookRepository { get; }
        public IRefreshTokenRepository RefreshTokenRepository { get; }
        public IUserAuthRepository UserAuthRepository { get; }
        public IUserProfileRepository UserProfileRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
