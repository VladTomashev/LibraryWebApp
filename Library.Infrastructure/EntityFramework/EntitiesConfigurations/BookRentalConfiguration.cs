using Library.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EntityFramework.EntitiesConfigurations
{
    public class BookRentalConfiguration : AbstractEntityConfiguration<BookRental>
    {
        public override void Configure(EntityTypeBuilder<BookRental> builder)
        {
            base.Configure(builder);
            builder.Property(br => br.BookId).IsRequired();
            builder.Property(br => br.UserProfileId).IsRequired();
            builder.Property(br => br.TakingTime).IsRequired();
            builder.Property(br => br.ReturnTime).IsRequired();
            builder.Property(br => br.IsReturned).IsRequired();

            builder.HasOne<Book>()
                   .WithMany()
                   .HasForeignKey(br => br.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<UserProfile>()
                   .WithMany()
                   .HasForeignKey(br => br.UserProfileId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
