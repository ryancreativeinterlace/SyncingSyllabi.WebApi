using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Settings
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
        public int MaximumRetry { get; set; }
        public int MaximumRetryIntervalSeconds { get; set; }
    }
}
