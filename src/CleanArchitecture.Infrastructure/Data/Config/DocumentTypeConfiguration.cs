using CleanArchitecture.Core;
using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Data.Config
{
    public class DocumentTypeConfiguration : BaseEntityConfiguration<DocumentType>
    {
        public override void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            base.Configure(builder);

            builder.Property(mt => mt.Name).IsRequired().HasMaxLength(ValidationRules.NameMaxLength);
        }
    }
}
