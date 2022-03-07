using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class UserEmailTrackingModel : BaseTrackedModel
    {
        public Int64 UserId { get; set; }
        public string Email { get; set; }
        public string EmailSubject { get; set; }
        public string EmailTemplate { get; set; }
        public string EmailStatus { get; set; }
    }
}
