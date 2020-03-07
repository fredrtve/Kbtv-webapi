using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class TimesheetWeekConfiguration : BaseEntityConfiguration<TimesheetWeek>
    {
        public override void Configure(EntityTypeBuilder<TimesheetWeek> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.UserName).IsRequired();
            builder.Property(t => t.Year).IsRequired();
            builder.Property(t => t.WeekNr).IsRequired();
            builder.Property(t => t.Status).IsRequired()
                   .HasConversion(v => 
                        v.ToString(), 
                        v => (TimesheetStatus)Enum.Parse(typeof(TimesheetStatus), v));
        }
    }
}
