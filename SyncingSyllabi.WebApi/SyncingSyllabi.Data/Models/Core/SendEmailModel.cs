using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SyncingSyllabi.Data.Models.Core
{
    public class SendEmailModel
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public IEnumerable<string> Cc { get; set; }
        public IEnumerable<string> XModel { get; set; }
        public string S3TemplateFile { get; set; }
        public IEnumerable<EmailAttachmentModel> Attachment { get; set; }
    }

    public class EmailAttachmentModel
    {
        public string FileName { get; set; }
        public string Type { get; set; }
        public string FileData { get; set; }
    }
}
