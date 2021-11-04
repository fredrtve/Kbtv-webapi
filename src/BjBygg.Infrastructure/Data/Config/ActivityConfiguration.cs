using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class ActivityConfiguration : BaseEntityConfiguration<Activity>
    {
        public override void Configure(EntityTypeBuilder<Activity> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
