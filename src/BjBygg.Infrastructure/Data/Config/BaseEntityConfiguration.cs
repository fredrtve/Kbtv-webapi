using BjBygg.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(m => m.Deleted == false);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
