using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Settings
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
