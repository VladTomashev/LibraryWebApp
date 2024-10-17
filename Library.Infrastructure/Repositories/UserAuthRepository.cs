using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class UserAuthRepository : Repository<UserAuth>, IUserAuthRepository
    {
        public UserAuthRepository(DataContext db) : base(db)
        {
        }

        public async Task<UserAuth> GetByLoginAsync(string login, CancellationToken cancellationToken = default)
        {
            return await db.UserAuths.Where(b => b.Login == login).FirstAsync(cancellationToken);
        }
    }
}
