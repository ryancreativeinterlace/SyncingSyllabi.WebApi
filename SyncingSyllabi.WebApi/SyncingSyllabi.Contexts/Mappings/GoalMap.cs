using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class GoalMap : BaseTrackedEntityMap<GoalEntity>
    {
        public GoalMap()
        {

        }
        public override void Configure(EntityTypeBuilder<GoalEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("goals");

            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.GoalTitle).HasColumnName("goal_title");
            builder.Property(e => e.GoalDescription).HasColumnName("goal_description");
            builder.Property(e => e.GoalType).HasColumnName("goal_type");
            builder.Property(e => e.GoalTypeName).HasColumnName("goal_type_name");
            builder.Property(e => e.GoalDateStart).HasColumnName("goal_date_start");
            builder.Property(e => e.GoalDateEnd).HasColumnName("goal_date_end");
            builder.Property(e => e.IsCompleted).HasColumnName("is_completed");
            builder.Property(e => e.IsArchived).HasColumnName("is_archived");
        }
    }
}
