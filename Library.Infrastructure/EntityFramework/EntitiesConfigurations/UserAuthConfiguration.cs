using Library.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.EntityFramework.EntitiesConfigurations
{
    public class UserAuthConfiguration : AbstractEntityConfiguration<UserAuth>
    {
        public override void Configure(EntityTypeBuilder<UserAuth> builder)
        {
            base.Configure(builder);
            builder.Property(ua => ua.Login)
                   .IsRequired()
                   .HasMaxLength(50);  

            builder.Property(ua => ua.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(ua => ua.Role)
                   .IsRequired()
                   .HasConversion<int>();
        }
    }
}
