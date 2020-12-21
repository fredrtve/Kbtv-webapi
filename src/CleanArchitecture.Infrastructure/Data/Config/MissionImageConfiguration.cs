using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionImageConfiguration : BaseEntityConfiguration<MissionImage>
    {
        public override void Configure(EntityTypeBuilder<MissionImage> builder)
        {
            base.Configure(builder);

            builder.Property(mi => mi.FileName).IsRequired().HasMaxLength(ValidationRules.FileNameMaxLength);
            builder.Property(mi => mi.MissionId).IsRequired();
        }
    }
}
