using Library.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.EntityFramework.EntitiesConfigurations
{
    public class AuthorConfiguration : AbstractEntityConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(a => a.DateOfBirth)
                   .IsRequired();

            builder.Property(a => a.Country)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
