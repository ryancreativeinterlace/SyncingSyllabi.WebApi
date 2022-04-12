using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Data.Constants
{
    public class SyllabusConstants
    {
        public static IField[] All
        {
            get
            {
                return new IField[]
                {
                    new SyllabusFieldsIds()
                };
            }
        }

        public interface IField
        {
            int[] All();
            int ObjectId();
            string GetFieldName(int fieldId);
            string GetFieldValue(int fieldId, SyllabusModel syllabus);
        }

        public class ObjectIds
        {
            public const int SyllabusFieldsIds = 1;
        }

        public class SyllabusFieldsIds : IField
        {
            public const int Id = 1;
            public const int UserId = 2;
            public const int ClassCode = 3;
            public const int ClassName = 4;
            public const int TeacherName = 5;
            public const int ClassSchedule = 6;
            public const int ColorInHex = 7;
            public const int IsActive = 8;

            public int[] All()
            {
                return new int[]
                {
                    Id,
                    UserId,
                    ClassCode,
                    ClassName,
                    TeacherName,
                    ClassSchedule,
                    ColorInHex,
                    IsActive
                };
            }

            public List<int> SyllabusFields()
            {
                return new List<int>
                {
                    Id,
                    UserId,
                    ClassCode,
                    ClassName,
                    TeacherName,
                    ClassSchedule,
                    ColorInHex,
                    IsActive
                };
            }

            public int ObjectId()
            {
                return ObjectIds.SyllabusFieldsIds;
            }

            public static string GetName(int id)
            {
                foreach (var field in typeof(SyllabusFieldsIds).GetFields())
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
                    case Id: return "Syllabus Id";
                    case UserId: return "User Id";
                    case ClassCode: return "Class Code";
                    case ClassName: return "Class Name";
                    case TeacherName: return "Teacher Name";
                    case ClassSchedule: return "Class Schedule";
                    case ColorInHex: return "Color In Hex";
                    case IsActive: return "Active";

                    default: return "Unknown";
                }
            }

            public string GetFieldValue(int fieldId, SyllabusModel syllabus)
            {
                switch (fieldId)
                {
                    case Id: return syllabus.Id.ToString();
                    case UserId: return syllabus.UserId.ToString();
                    case ClassCode: return syllabus.ClassCode;
                    case ClassName: return syllabus.ClassName;
                    case TeacherName: return syllabus.TeacherName;
                    case ClassSchedule: return syllabus.ClassSchedule ?? string.Empty;
                    case ColorInHex: return syllabus.ColorInHex ?? string.Empty;
                    case IsActive: return syllabus.IsActive.ToString() ?? string.Empty;

                    default: return string.Empty;
                }
            }
        }
    }
}
