using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class UserNotificationMap : BaseTrackedEntityMap<UserNotificationEntity>
    {
        public UserNotificationMap()
        {

        }

        public override void Configure(EntityTypeBuilder<UserNotificationEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("user_notifications");

            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.Title).HasColumnName("title");
            builder.Property(e => e.Message).HasColumnName("message");
            builder.Property(e => e.NotificationStatus).HasColumnName("notification_status");
            builder.Property(e => e.NotificationStatusName).HasColumnName("notification_status_name");
        }
    }
}
