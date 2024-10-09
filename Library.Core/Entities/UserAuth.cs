using Library.Core.Enums;

namespace Library.Core.Entities
{
    public class UserAuth : AbstractEntity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; set; }
    }
}
