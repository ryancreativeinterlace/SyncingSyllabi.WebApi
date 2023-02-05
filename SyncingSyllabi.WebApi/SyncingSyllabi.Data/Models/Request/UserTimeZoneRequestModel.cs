using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class UserTimeZoneRequestModel
    {
        public Int64 UserId { get; set; }
        public string TimeZone { get; set; }
    }
}
