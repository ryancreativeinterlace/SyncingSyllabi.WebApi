using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class UserCodeMap : BaseTrackedEntityMap<UserCodeEntity>
    {
        public UserCodeMap()
        {

        }
        public override void Configure(EntityTypeBuilder<UserCodeEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("user_codes");

            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.VerificationCode).HasColumnName("verification_code");
            builder.Property(e => e.CodeType).HasColumnName("code_type");
            builder.Property(e => e.CodeTypeName).HasColumnName("code_type_name");
            builder.Property(e => e.CodeExpiration).HasColumnName("code_expiration");
        }
    }
}
