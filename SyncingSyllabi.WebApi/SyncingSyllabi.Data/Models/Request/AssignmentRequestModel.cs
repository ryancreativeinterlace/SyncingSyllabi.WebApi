using SyncingSyllabi.Data.Models.Request.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class AssignmentRequestModel : BaseListRequestModel
    {
        public Int64 AssignmentId { get; set; }
        public Int64 UserId { get; set; }
        public Int64? SyllabusId { get; set; }
        public string AssignmentTitle { get; set; }
        public string Notes { get; set; }
        public DateTime? AssignmentDateStart { get; set; }
        public DateTime? AssignmentDateEnd { get; set; }
        public string ColorInHex { get; set; }
        public bool? IsActive { get; set; }
    }
}
