using Library.Core.Entities;
using Library.Core.Interfaces;
using Library.Infrastructure.EntityFramework;

namespace Library.Infrastructure.Repositories
{
    public class UserProfileRepository : Repository<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(DataContext db) : base(db)
        {
        }
    }
}
