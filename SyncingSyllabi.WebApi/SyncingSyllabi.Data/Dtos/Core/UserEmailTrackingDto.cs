using SyncingSyllabi.Data.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class UserEmailTrackingDto : BaseTrackedDto
    {
        public Int64 UserId { get; set; }
        public string Email { get; set; }
        public string EmailSubject { get; set; }
        public string EmailTemplate { get; set; }
        public string EmailStatus { get; set; }
    }
}
