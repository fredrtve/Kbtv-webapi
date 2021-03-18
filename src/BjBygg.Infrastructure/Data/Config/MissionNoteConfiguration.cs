using BjBygg.Core;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class MissionNoteConfiguration : BaseEntityConfiguration<MissionNote>
    {
        public override void Configure(EntityTypeBuilder<MissionNote> builder)
        {
            base.Configure(builder);

            builder.Property(mn => mn.Title).HasMaxLength(ValidationRules.MissionNoteTitleMaxLength);
            builder.Property(mn => mn.Content).IsRequired().HasMaxLength(ValidationRules.MissionNoteContentMaxLength);
            builder.Property(mn => mn.MissionId).IsRequired();
        }
    }
}
