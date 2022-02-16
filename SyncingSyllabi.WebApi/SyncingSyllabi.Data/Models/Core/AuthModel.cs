using System;
using SyncingSyllabi.Data.Models.Base;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class AuthModel : BaseTrackedModel
    {
        public Int64 UserId { get; set; } 
        public string AuthToken { get; set; }
        public DateTime? AuthTokenExpiration { get; set; }
    }
}
