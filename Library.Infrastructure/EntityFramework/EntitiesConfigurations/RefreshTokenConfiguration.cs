using Library.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EntityFramework.EntitiesConfigurations
{
    public class RefreshTokenConfiguration : AbstractEntityConfiguration<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);

            builder.Property(rt => rt.Token)
                   .HasMaxLength(500);  

            builder.HasOne<UserAuth>()
                   .WithOne()
                   .HasForeignKey<RefreshToken>(rt => rt.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
