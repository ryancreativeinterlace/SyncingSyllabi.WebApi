using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Settings
{
    public class S3Settings
    {
        public string AccessKeyId { get; set; } = "AKIAZB4P3VRPIFBKMMK4";
        public string SecretAccessKey { get; set; } = "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";
        public string Region { get; set; }
        public string BucketName { get; set; }
        public string UserFileDirectory { get; set; }
        public string EmailTemplateDirectory { get; set; }
    }
}
