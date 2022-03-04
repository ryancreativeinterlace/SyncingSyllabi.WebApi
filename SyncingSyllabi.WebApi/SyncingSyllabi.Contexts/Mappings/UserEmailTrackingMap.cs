using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Contexts.Mappings.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Mappings
{
    public class UserEmailTrackingMap : BaseTrackedEntityMap<UserEmailTrackingEntity>
    {
        public UserEmailTrackingMap()
        {

        }
        public override void Configure(EntityTypeBuilder<UserEmailTrackingEntity> builder)
        {
            base.Configure(builder);

            builder.ToTable("user_email_tracking");

            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.Email).HasColumnName("email");
            builder.Property(e => e.EmailSubject).HasColumnName("email_subject");
            builder.Property(e => e.EmailTemplate).HasColumnName("email_template");
            builder.Property(e => e.EmailStatus).HasColumnName("email_status");
        }
    }
}
