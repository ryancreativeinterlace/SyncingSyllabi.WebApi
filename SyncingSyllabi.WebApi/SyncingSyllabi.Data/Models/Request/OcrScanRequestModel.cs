using Microsoft.AspNetCore.Http;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class OcrScanRequestModel
    {
        public Int64 UserId { get; set; }
        public OcrTypeEnum OcrTypeEnum { get; set; }
        public OcrUploadTypeEnum OcrUploadTypeEnum { get; set; }
        public IEnumerable<int> Pages { get; set; }
        public byte[] FileByte { get; set; }
        public IFormFile ImageFile { get; set; }
        public IEnumerable<string> PdfFile { get; set; }
    }
}
