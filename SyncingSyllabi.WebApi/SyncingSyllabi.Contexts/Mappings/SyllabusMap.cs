using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class SyllabusMap : BaseTrackedEntityMap<SyllabusEntity>
    {
        public SyllabusMap()
        {

        }
        public override void Configure(EntityTypeBuilder<SyllabusEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("syllabus");

            builder.Property(e => e.ClassCode).HasColumnName("class_code");
            builder.Property(e => e.ClassName).HasColumnName("class_name");
            builder.Property(e => e.TeacherName).HasColumnName("teacher_name");
        }
    }
}
