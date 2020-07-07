using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionTypeConfiguration : BaseEntityConfiguration<MissionType>
    {
        public override void Configure(EntityTypeBuilder<MissionType> builder)
        {
            base.Configure(builder);

            builder.Property(mt => mt.Name).IsRequired().HasMaxLength(45);
        }
    }
}
