using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities.Base;
using SyncingSyllabi.Contexts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings.Base
{
    public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasColumnName("id");
            builder.Property(t => t.IsActive).HasColumnName("is_active");
        }
    }
}
