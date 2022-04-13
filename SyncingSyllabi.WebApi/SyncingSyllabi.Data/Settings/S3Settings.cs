using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Settings
{
    public class S3Settings
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
        public string Region { get; set; }
        public string BucketName { get; set; }
        public string UserFileDirectory { get; set; }
        public string EmailTemplateDirectory { get; set; }
        public string SyllabusFilesDirectory { get; set; }
    }
}
