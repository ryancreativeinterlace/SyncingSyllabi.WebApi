﻿using SyncingSyllabi.Contexts.Entities.Base;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities
{
    public class UserNotificationEntity : BaseTrackedEntity
    {
        public Int64 UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationStatusEnum NotificationStatus { get; set; }
        public string NotificationStatusName { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
        public string NotificationTypeName { get; set; }
        public Int64 ReferenceId { get; set; }
        public bool? IsRead { get; set; }
    }
}
