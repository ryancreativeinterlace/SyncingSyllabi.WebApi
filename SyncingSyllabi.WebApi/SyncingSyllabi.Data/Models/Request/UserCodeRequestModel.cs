using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class UserCodeRequestModel
    {
        public Int64 UserId { get; set; }
        public CodeTypeEnum CodeType { get; set; }
        public string VerificationCode { get; set; }
        public bool IsResend { get; set; }
    }
}
