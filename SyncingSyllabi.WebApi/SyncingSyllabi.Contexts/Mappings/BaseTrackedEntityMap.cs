using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public abstract class BaseTrackedEntityMap<T> : BaseMap<T> where T : BaseTrackedEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.Property(t => t.DateCreated).HasColumnName("created_on");
            builder.Property(t => t.CreatedBy).HasColumnName("created_by");
            builder.Property(t => t.DateUpdated).HasColumnName("updated_on");
            builder.Property(t => t.UpdatedBy).HasColumnName("updated_by");
        }
    }
}
