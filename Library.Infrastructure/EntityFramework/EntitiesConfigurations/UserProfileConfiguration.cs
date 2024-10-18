using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.EntityFramework.EntitiesConfigurations
{
    public class UserProfileConfiguration : AbstractEntityConfiguration<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.Property(up => up.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);  

            builder.Property(up => up.LastName)
                   .IsRequired()
                   .HasMaxLength(50);  

            builder.Property(up => up.Phone)
                   .IsRequired()
                   .HasMaxLength(30);  

            builder.HasOne<UserAuth>()
                   .WithOne()
                   .HasForeignKey<UserProfile>(up => up.Id)  
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
