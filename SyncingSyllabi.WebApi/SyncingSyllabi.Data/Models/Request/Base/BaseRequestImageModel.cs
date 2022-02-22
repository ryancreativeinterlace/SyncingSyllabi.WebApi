using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request.Base
{
    public class BaseRequestImageModel
    {
        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageByte { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
