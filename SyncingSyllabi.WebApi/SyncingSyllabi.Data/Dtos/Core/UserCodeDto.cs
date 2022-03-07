using SyncingSyllabi.Data.Dtos.Base;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class UserCodeDto : BaseTrackedDto
    {
        public Int64 UserId { get; set; }
        public string VerificationCode { get; set; }
        public CodeTypeEnum CodeType { get; set; }
        public string CodeTypeName { get; set; }
        public DateTime? CodeExpiration { get; set; }
    }
}
