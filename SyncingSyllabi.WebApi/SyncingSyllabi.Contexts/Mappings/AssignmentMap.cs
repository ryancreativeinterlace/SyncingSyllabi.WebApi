using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class AssignmentMap : BaseTrackedEntityMap<AssignmentEntity>
    {
        public AssignmentMap()
        {

        }
        public override void Configure(EntityTypeBuilder<AssignmentEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("assignments");

            builder.Property(e => e.SyllabusId).HasColumnName("syllabus_id");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.AssignmentTitle).HasColumnName("assignment_title");
            builder.Property(e => e.Notes).HasColumnName("notes");
            builder.Property(e => e.AssignmentDateStart).HasColumnName("assignment_date_start");
            builder.Property(e => e.AssignmentDateEnd).HasColumnName("assignment_date_end");
            builder.Property(e => e.ColorInHex).HasColumnName("color_in_hex");
            builder.Property(e => e.IsCompleted).HasColumnName("is_completed");
            builder.Property(e => e.Attachment).HasColumnName("attachment");
        }
    }
}
