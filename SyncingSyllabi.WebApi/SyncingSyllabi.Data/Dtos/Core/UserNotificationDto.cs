using SyncingSyllabi.Data.Dtos.Base;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class UserNotificationDto : BaseTrackedDto
    {
        public Int64 UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationStatusEnum NotificationStatus { get; set; }
        public string NotificationStatusName { get; set; }
        public bool? IsRead { get; set; }
    }
}
