using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;

namespace Library.Infrastructure.Repositories
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DataContext db) : base(db)
        {
        }
    }
}
