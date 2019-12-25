using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionImageConfiguration : BaseEntityConfiguration<MissionImage>
    {
        public override void Configure(EntityTypeBuilder<MissionImage> builder)
        {
            base.Configure(builder);

            builder.Property(mi => mi.FileURL).IsRequired().HasMaxLength(400);
            builder.Property(mi => mi.MissionId).IsRequired();

            builder.Property(mi => mi.FileURL).HasConversion(v => v.ToString(), v => new Uri(v));
        }
    }
}
