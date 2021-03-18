using BjBygg.Core;
using BjBygg.Core.Entities;
using BjBygg.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BjBygg.Infrastructure.Data.Config
{
    public class TimeSheetConfiguration : BaseEntityConfiguration<Timesheet>
    {
        public override void Configure(EntityTypeBuilder<Timesheet> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.UserName).IsRequired();
            builder.Property(t => t.StartTime).IsRequired();
            builder.Property(t => t.EndTime).IsRequired();
            builder.Property(t => t.Comment).IsRequired().HasMaxLength(ValidationRules.TimesheetCommentMaxLength);
            builder.Property(t => t.MissionId).IsRequired();
            builder.Property(t => t.Status).IsRequired()
                   .HasConversion(v =>
                        v.ToString(),
                        v => (TimesheetStatus)Enum.Parse(typeof(TimesheetStatus), v));
        }
    }
}
