using SyncingSyllabi.Contexts.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities
{
    public class AuthTokenEntity: BaseTrackedEntity
    {
        public Int64 UserId { get; set; }
        public string AuthToken { get; set; }
        public DateTime? AuthTokenExpiration { get; set; }
    }
}
