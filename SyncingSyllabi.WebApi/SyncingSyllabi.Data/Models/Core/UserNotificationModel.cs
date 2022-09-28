using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class UserNotificationModel : BaseTrackedModel
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
