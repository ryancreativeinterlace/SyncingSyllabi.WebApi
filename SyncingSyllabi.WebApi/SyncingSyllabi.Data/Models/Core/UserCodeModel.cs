using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class UserCodeModel : BaseTrackedModel
    {
        public Int64 UserId { get; set; }
        public string VerificationCode { get; set; }
        public CodeTypeEnum CodeType { get; set; }
        public string CodeTypeName { get; set; }
        public DateTime? CodeExpiration { get; set; }
    }
}
