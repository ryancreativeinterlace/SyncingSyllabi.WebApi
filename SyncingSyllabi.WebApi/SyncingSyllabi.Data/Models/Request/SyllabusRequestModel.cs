using Microsoft.AspNetCore.Http;
using SyncingSyllabi.Data.Models.Request.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class SyllabusRequestModel : BaseListRequestModel
    {
        public Int64 SyllabusId { get; set; }
        public Int64 UserId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public string ClassSchedule { get; set; }
        public string ColorInHex { get; set; }
        public bool? IsActive { get; set; }

        public string ImageName { get; set; }
        public string ImageUrl { get; set; }
        public byte[] ImageByte { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
