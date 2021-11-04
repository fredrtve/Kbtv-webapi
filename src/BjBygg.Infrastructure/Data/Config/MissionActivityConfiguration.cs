using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class MissionActivityConfiguration : BaseEntityConfiguration<MissionActivity>
    {
        public override void Configure(EntityTypeBuilder<MissionActivity> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.MissionId).IsRequired();
            builder.Property(e => e.ActivityId).IsRequired();
        }
    }
}
