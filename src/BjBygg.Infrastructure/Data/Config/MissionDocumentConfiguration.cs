﻿using BjBygg.Core;
using BjBygg.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BjBygg.Infrastructure.Data.Config
{
    public class MissionDocumentConfiguration : BaseEntityConfiguration<MissionDocument>
    {
        public override void Configure(EntityTypeBuilder<MissionDocument> builder)
        {
            base.Configure(builder);

            builder.Property(mi => mi.Name).IsRequired().HasMaxLength(ValidationRules.NameMaxLength);
            builder.Property(mi => mi.FileName).IsRequired().HasMaxLength(ValidationRules.FileNameMaxLength);
            builder.Property(mi => mi.MissionId).IsRequired();
        }
    }
}
