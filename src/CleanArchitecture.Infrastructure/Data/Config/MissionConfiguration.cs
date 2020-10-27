using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class MissionConfiguration : BaseEntityConfiguration<Mission>
    {
        public override void Configure(EntityTypeBuilder<Mission> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(ValidationRules.AddressMaxLength);
            builder.Property(mi => mi.FileName).HasMaxLength(ValidationRules.FileNameMaxLength);
            builder.Property(e => e.PhoneNumber).HasMaxLength(ValidationRules.PhoneNumberMaxLength);
            builder.Property(e => e.Description).HasMaxLength(ValidationRules.MissionDescriptionMaxLength);
            builder.Property(e => e.Finished).HasDefaultValue(false);
        }
    }
}
