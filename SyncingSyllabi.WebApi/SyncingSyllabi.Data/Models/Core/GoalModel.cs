using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Core
{
    public class GoalModel : BaseTrackedModel
    {
        public Int64 UserId { get; set; }
        public string GoalTitle { get; set; }
        public string GoalDescription { get; set; }
        public GoalTypeEnum GoalType { get; set; }
        public string GoalTypeName { get; set; }
        public DateTime? GoalDateStart { get; set; }
        public DateTime? GoalDateEnd { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsArchived { get; set; }
    }
}
