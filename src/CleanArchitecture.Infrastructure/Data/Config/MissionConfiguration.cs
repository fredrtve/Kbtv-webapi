using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionConfiguration : BaseEntityConfiguration<Mission>
    {
        public override void Configure(EntityTypeBuilder<Mission> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Name).HasMaxLength(50);
            builder.Property(mi => mi.ImageURL).HasMaxLength(400);
            builder.Property(e => e.PhoneNumber).HasMaxLength(12);
            builder.Property(e => e.Description).HasMaxLength(400);
            builder.Property(e => e.Finished).HasDefaultValue(false);
        }
    }
}
