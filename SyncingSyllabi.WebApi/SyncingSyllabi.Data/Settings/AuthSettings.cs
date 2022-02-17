using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Settings
{
    public class AuthSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpirationInMinutes { get; set; }
    }
}
