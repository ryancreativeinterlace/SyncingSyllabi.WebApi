using SyncingSyllabi.Contexts.Entities.Base;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Contexts.Entities
{
    public class GoalEntity : BaseTrackedEntity
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
