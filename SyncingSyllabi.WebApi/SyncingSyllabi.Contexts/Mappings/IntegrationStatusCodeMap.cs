using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class IntegrationStatusCodeMap : BaseMap<IntegrationStatusCode>
    {
        public IntegrationStatusCodeMap()
        {
        }

        public override void Configure(EntityTypeBuilder<IntegrationStatusCode> builder)
        {
            base.Configure(builder);

            builder.ToTable("integration_status_codes");
            builder.Property(t => t.IntegrationId).HasColumnName("integration_id");
            builder.Property(t => t.StatusCode).HasColumnName("status_code");
        }
    }
}
