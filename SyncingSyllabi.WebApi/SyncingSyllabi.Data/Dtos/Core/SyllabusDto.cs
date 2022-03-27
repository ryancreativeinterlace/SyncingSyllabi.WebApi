using SyncingSyllabi.Data.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class SyllabusDto : BaseTrackedDto
    {
        public Int64 UserId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public DateTime? ClassSchedule { get; set; }
        public string ColorInHex { get; set; }
    }
}
