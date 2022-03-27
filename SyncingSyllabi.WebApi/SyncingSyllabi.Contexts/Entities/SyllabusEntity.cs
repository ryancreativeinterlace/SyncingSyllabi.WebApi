using SyncingSyllabi.Contexts.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities
{
    public class SyllabusEntity : BaseTrackedEntity
    {
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
    }
}
