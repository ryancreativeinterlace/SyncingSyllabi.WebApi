using SyncingSyllabi.Contexts.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities
{
    public class AssignmentEntity : BaseTrackedEntity
    {
        public Int64 SyllabusId { get; set; }
        public Int64 UserId { get; set; }
        public string AssignmentTitle { get; set; }
        public string Notes { get; set; }
        public DateTime? AssignmentDateStart { get; set; }
        public DateTime? AssignmentDateEnd { get; set; }
        public string ColorInHex { get; set; }
        public bool? IsCompleted { get; set; }
        public string AttachmentFileName { get; set; }
        public string Attachment { get; set; }
    }
}
