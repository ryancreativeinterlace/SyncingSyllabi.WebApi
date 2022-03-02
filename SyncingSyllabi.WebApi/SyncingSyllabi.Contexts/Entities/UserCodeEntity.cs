using SyncingSyllabi.Contexts.Entities.Base;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities
{
    public class UserCodeEntity : BaseTrackedEntity
    {
        public Int64 UserId { get; set; }
        public string VerificationCode { get; set; }
        public CodeTypeEnum CodeType { get; set; }
        public string CodeTypeName { get; set; }
        public DateTime? CodeExpiration { get; set; }
    }
}
