using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionReportConfiguration : BaseEntityConfiguration<MissionReport>
    {
        public override void Configure(EntityTypeBuilder<MissionReport> builder)
        {
            base.Configure(builder);

            builder.Property(mi => mi.FileURL).IsRequired().HasMaxLength(400);
            builder.Property(mi => mi.MissionId).IsRequired();

            builder.Property(mi => mi.FileURL).HasConversion(v => v.ToString(), v => new Uri(v));
        }
    }
}
