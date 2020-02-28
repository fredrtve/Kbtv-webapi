using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class TimesheetConfiguration : BaseEntityConfiguration<Timesheet>
    {
        public override void Configure(EntityTypeBuilder<Timesheet> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.StartTime).IsRequired();
            builder.Property(t => t.EndTime).IsRequired();
            builder.Property(t => t.MissionId).IsRequired();
            builder.Property(t => t.Locked).HasDefaultValue(false);
        }
    }
}
