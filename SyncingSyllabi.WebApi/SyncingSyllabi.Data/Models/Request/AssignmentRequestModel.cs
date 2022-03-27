using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class AssignmentRequestModel
    {
        public Int64 SyllabusId { get; set; }
        public Int64 UserId { get; set; }
        public string Notes { get; set; }
        public DateTime? AssignmentDateStart { get; set; }
        public DateTime? AssignmentDateEnd { get; set; }
        public bool? IsActive { get; set; }
    }
}
