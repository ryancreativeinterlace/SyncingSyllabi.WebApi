using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class NotificationTokenRequestModel
    {
        public Int64 UserId { get; set; }
        public string NotificationToken { get; set; }
    }
}
