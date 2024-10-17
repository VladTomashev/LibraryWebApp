using Library.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EntityFramework.EntitiesConfigurations
{
    public class AbstractEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : AbstractEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
