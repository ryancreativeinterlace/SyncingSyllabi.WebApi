using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Constants
{
    public class AssignmentConstants
    {
        public static IField[] All
        {
            get
            {
                return new IField[]
                {
                    new AssignmentFieldsIds()
                };
            }
        }

        public interface IField
        {
            int[] All();
            int ObjectId();
            string GetFieldName(int fieldId);
            string GetFieldValue(int fieldId, AssignmentModel assignment);
        }

        public class ObjectIds
        {
            public const int AssignmentFieldsIds = 1;
        }

        public class AssignmentFieldsIds : IField
        {
            public const int Id = 1;
            public const int SyllabusId = 2;
            public const int UserId = 3;
            public const int Notes = 4;
            public const int ColorInHex = 5;
            public const int AssignmentDateStart = 6;
            public const int AssignmentDateEnd = 7;
            public const int IsActive = 8;

            public int[] All()
            {
                return new int[]
                {
                    Id,
                    SyllabusId,
                    UserId,
                    Notes,
                    ColorInHex,
                    AssignmentDateStart,
                    AssignmentDateEnd,
                    IsActive
                };
            }

            public List<int> AssignmentFields()
            {
                return new List<int>
                {
                    Id,
                    SyllabusId,
                    UserId,
                    Notes,
                    ColorInHex,
                    AssignmentDateStart,
                    AssignmentDateEnd,
                    IsActive
                };
            }

            public int ObjectId()
            {
                return ObjectIds.AssignmentFieldsIds;
            }

            public static string GetName(int id)
            {
                foreach (var field in typeof(AssignmentFieldsIds).GetFields())
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
                    case Id: return "Assignment Id";
                    case SyllabusId: return "Syllabus Id";
                    case UserId: return "User Id";
                    case Notes: return "Notes";
                    case ColorInHex: return "Color In Hex";
                    case AssignmentDateStart: return "Assignment Date Start";
                    case AssignmentDateEnd: return "Assignment Date End";
                    case IsActive: return "Active";

                    default: return "Unknown";
                }
            }

            public string GetFieldValue(int fieldId, AssignmentModel assignment)
            {
                switch (fieldId)
                {
                    case Id: return assignment.Id.ToString();
                    case SyllabusId: return assignment.SyllabusId.ToString();
                    case UserId: return assignment.UserId.ToString();
                    case Notes: return assignment.Notes.ToString() ?? string.Empty;
                    case AssignmentDateStart: return assignment.AssignmentDateStart.Value.ToString("M/d/yyyy h:mm:ss tt") ?? string.Empty;
                    case AssignmentDateEnd: return assignment.AssignmentDateEnd.Value.ToString("M/d/yyyy h:mm:ss tt") ?? string.Empty;
                    case ColorInHex: return assignment.ColorInHex ?? string.Empty;
                    case IsActive: return assignment.IsActive.ToString() ?? string.Empty;

                    default: return string.Empty;
                }
            }
        }
    }
}
