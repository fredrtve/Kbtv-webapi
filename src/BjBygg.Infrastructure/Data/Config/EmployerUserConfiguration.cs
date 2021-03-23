using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class EmployerUserConfiguration : BaseEntityConfiguration<EmployerUser>
    {
        public override void Configure(EntityTypeBuilder<EmployerUser> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.UserName).IsRequired();
            builder.Property(b => b.EmployerId).IsRequired();
        }
    }
}
