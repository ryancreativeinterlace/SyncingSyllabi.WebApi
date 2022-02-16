using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class AuthTokenMap : BaseTrackedEntityMap<AuthTokenEntity>
    {
        public AuthTokenMap()
        {

        }
        public override void Configure(EntityTypeBuilder<AuthTokenEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("auth_tokens");

            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.AuthToken).HasColumnName("auth_token");
            builder.Property(e => e.AuthTokenExpiration).HasColumnName("auth_token_expiration");
        }
    }
}
