using Library.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.EntityFramework.EntitiesConfigurations
{
    public class BookConfiguration : AbstractEntityConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Isbn)
                   .IsRequired()
                   .HasMaxLength(13);  

            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(50);  

            builder.Property(b => b.Genre)
                   .IsRequired()
                   .HasMaxLength(50);  

            builder.Property(b => b.Description)
                   .IsRequired()
                   .HasMaxLength(500);  

            builder.HasOne<Author>()
                   .WithMany()
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
