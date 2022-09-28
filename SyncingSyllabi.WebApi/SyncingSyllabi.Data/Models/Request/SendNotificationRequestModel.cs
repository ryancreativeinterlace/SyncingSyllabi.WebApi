using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class SendNotificationRequestModel
    {
        public Int64 UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public Int64 ReferenceId { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
    }
}
