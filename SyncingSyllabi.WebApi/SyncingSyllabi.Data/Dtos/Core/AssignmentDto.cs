using SyncingSyllabi.Data.Dtos.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class AssignmentDto : BaseTrackedDto
    {
        public Int64 SyllabusId { get; set; }
        public Int64 UserId { get; set; }
        public string Notes { get; set; }
        public DateTime? AssignmentDateStart { get; set; }
        public DateTime? AssignmentDateEnd { get; set; }
    }
}
