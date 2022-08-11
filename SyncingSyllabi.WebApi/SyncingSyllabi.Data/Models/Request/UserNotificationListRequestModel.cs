using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Request.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class UserNotificationListRequestModel : BaseListRequestModel
    {
        public Int64 UserId { get; set; }
        public UserNotificationStatusEnum UserNotificationStatus { get; set; }
    }
}
