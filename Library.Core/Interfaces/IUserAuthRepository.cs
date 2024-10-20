using Library.Core.Entities;

namespace Library.Core.Interfaces
{
    public interface IUserAuthRepository : IRepository<UserAuth>
    {
        public Task<UserAuth> GetByLoginAsync(string login, CancellationToken cancellationToken = default);
    }
}
