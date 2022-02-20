using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Request
{
    public class GoalRequestModel
    {
        public long GoalId { get; set; }
        public long UserId { get; set; }
        public string GoalTitle {get; set;}
        public int? GoalType { get; set; }
        public string GoalDescription { get; set; }
        public DateTime? GoalDateStart { get; set; }
        public DateTime? GoalDateEnd { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsAchieved { get; set; }
    }
}
