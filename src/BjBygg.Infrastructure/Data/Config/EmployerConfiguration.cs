using BjBygg.Core;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class EmployerConfiguration : BaseEntityConfiguration<Employer>
    {
        public override void Configure(EntityTypeBuilder<Employer> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(ValidationRules.NameMaxLength);
            builder.Property(e => e.PhoneNumber).HasMaxLength(ValidationRules.PhoneNumberMaxLength);
            builder.Property(e => e.Address).HasMaxLength(ValidationRules.AddressMaxLength);
        }
    }
}
