using SyncingSyllabi.Data.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class AuthTokenDto : BaseTrackedDto
    {
        public Int64 UserId { get; set; }
        public string AuthToken { get; set; }
        public DateTime? AuthTokenExpiration { get; set; }
    }
}
