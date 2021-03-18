using BjBygg.Core;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class MissionTypeConfiguration : BaseEntityConfiguration<MissionType>
    {
        public override void Configure(EntityTypeBuilder<MissionType> builder)
        {
            base.Configure(builder);

            builder.Property(mt => mt.Name).IsRequired().HasMaxLength(ValidationRules.NameMaxLength);
        }
    }
}
