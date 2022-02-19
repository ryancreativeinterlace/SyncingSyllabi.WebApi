using SyncingSyllabi.Data.Dtos.Base;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Dtos.Core
{
    public class GoalDto : BaseTrackedDto
    {
        public Int64 UserId { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public GoalTypeEnum GoalType { get; set; }
        public string GoalTypeName { get; set; }
        public DateTime? GoalDateStart { get; set; }
        public DateTime? GoalDateEnd { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsArchived { get; set; }
    }
}
