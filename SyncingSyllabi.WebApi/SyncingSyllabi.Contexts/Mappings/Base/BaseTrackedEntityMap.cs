using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings.Base
{
    public abstract class BaseTrackedEntityMap<T> : BaseMap<T> where T : BaseTrackedEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.DateCreated).HasColumnName("date_created");
            builder.Property(t => t.CreatedBy).HasColumnName("created_by");
            builder.Property(t => t.DateUpdated).HasColumnName("date_updated");
            builder.Property(t => t.UpdatedBy).HasColumnName("updated_by");
        }
    }
}
