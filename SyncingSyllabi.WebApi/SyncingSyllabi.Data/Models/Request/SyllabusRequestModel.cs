using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class SyllabusRequestModel
    {
        public Int64 SyllabusId { get; set; }
        public Int64 UserId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public DateTime? ClassSchedule { get; set; }
        public string ColorInHex { get; set; }
        public bool? IsActive { get; set; }
    }
}
