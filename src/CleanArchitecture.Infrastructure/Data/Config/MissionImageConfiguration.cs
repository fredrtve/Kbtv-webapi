using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionImageConfiguration : BaseEntityConfiguration<MissionImage>
    {
        public override void Configure(EntityTypeBuilder<MissionImage> builder)
        {
            base.Configure(builder);

            builder.Property(mi => mi.FileName).IsRequired().HasMaxLength(40);
            builder.Property(mi => mi.MissionId).IsRequired();
        }
    }
}
