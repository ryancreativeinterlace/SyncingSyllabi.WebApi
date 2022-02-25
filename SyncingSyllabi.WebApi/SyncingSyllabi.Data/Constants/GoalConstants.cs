using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Constants
{
    public class GoalConstants
    {
        public static IField[] All
        {
            get
            {
                return new IField[]
                {
                    new GoaldFieldsIds()
                };
            }
        }

        public interface IField
        {
            int[] All();
            int ObjectId();
            string GetFieldName(int fieldId);
            string GetFieldValue(int fieldId, GoalModel goal);
        }

        public class ObjectIds
        {
            public const int GoalFieldsIds = 1;
        }

        public class GoaldFieldsIds : IField
        {
            public const int Id = 1;
            public const int UserId = 2;
            public const int GoalTitle = 3;
            public const int GoalDescription = 4;
            public const int GoalType = 5;
            public const int GoalDateStart = 6;
            public const int GoalDateEnd = 7;
            public const int IsCompleted = 8;
            public const int IsArchived = 9;
            public const int IsActive = 10;

            public int[] All()
            {
                return new int[]
                {
                    Id,
                    UserId,
                    GoalTitle,
                    GoalDescription,
                    GoalType,
                    GoalDateStart,
                    GoalDateEnd,
                    IsCompleted,
                    IsArchived,
                    IsActive
                };
            }

            public List<int> GoalFields()
            {
                return new List<int>
                {
                    Id,
                    UserId,
                    GoalTitle,
                    GoalDescription,
                    GoalType,
                    GoalDateStart,
                    GoalDateEnd,
                    IsCompleted,
                    IsArchived,
                    IsActive
                };
            }

            public int ObjectId()
            {
                return ObjectIds.GoalFieldsIds;
            }

            public static string GetName(int id)
            {
                foreach (var field in typeof(GoaldFieldsIds).GetFields())
                {
                    if ((int)field.GetValue(null) == id)
                        return field.Name;
                }
                return "";
            }

            public string GetFieldName(int fieldId)
            {
                switch (fieldId)
                {
                    case Id: return "Goal Id";
                    case UserId: return "User Id";
                    case GoalTitle: return "Goal Title";
                    case GoalDescription: return "Goal Description";
                    case GoalType: return "Goal Type";
                    case GoalDateStart: return "Goal Date Start";
                    case GoalDateEnd: return "Goal Date End";
                    case IsCompleted: return "Completed";
                    case IsArchived: return "Archieved";
                    case IsActive: return "Active";

                    default: return "Unknown";
                }
            }

            public string GetFieldValue(int fieldId, GoalModel goal)
            {
                switch (fieldId)
                {
                    case Id: return goal.Id.ToString();
                    case UserId: return goal.UserId.ToString();
                    case GoalTitle: return goal.GoalTitle;
                    case GoalDescription: return goal.GoalDescription;
                    case GoalType: return this.GetLeadStatus(goal.GoalType);
                    case GoalDateStart: return goal.GoalDateStart.Value.ToString("M/d/yyyy h:mm:ss tt") ?? string.Empty;
                    case GoalDateEnd: return goal.GoalDateEnd.Value.ToString("M/d/yyyy h:mm:ss tt") ?? string.Empty;
                    case IsCompleted: return goal.IsCompleted.ToString() ?? string.Empty;
                    case IsArchived: return goal.IsArchived.ToString() ?? string.Empty;
                    case IsActive: return goal.IsActive.ToString() ?? string.Empty;

                    default: return string.Empty;
                }
            }

            private string GetLeadStatus(GoalTypeEnum goalType)
            {
                switch (goalType)
                {
                    case GoalTypeEnum.ShortTerm: return "Short-Term";
                    case GoalTypeEnum.MediumTerm: return "Medium-Term";
                    case GoalTypeEnum.LongTerm: return "Long-Term";

                    default: return string.Empty;
                }
            }
        }
    }
}
