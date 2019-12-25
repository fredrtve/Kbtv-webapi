using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionNoteConfiguration : BaseEntityConfiguration<MissionNote>
    {
        public override void Configure(EntityTypeBuilder<MissionNote> builder)
        {
            base.Configure(builder);

            builder.Property(mn => mn.Title).HasMaxLength(75);
            builder.Property(mn => mn.Content).IsRequired().HasMaxLength(400);
            builder.Property(mn => mn.MissionId).IsRequired();
        }
    }
}
