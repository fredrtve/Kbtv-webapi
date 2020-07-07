using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class EmployerConfiguration : BaseEntityConfiguration<Employer>
    {
        public override void Configure(EntityTypeBuilder<Employer> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
            builder.Property(e => e.PhoneNumber).HasMaxLength(12);
            builder.Property(e => e.Address).HasMaxLength(100);
        }
    }
}
