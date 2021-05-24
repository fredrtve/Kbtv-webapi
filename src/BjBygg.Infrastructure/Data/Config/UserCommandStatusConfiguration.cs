using BjBygg.Core.Entities;
using BjBygg.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class UserCommandStatusConfiguration : IEntityTypeConfiguration<UserCommandStatus>
    {
        public virtual void Configure(EntityTypeBuilder<UserCommandStatus> builder)
        {
            builder.HasKey(x => x.UserName);
        }
    }
}
