using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class OcrScanResponseModel : BaseResponseModel<OcrScanReponseDataModel>
    {
        public OcrScanResponseModel()
        {
            Data = new OcrScanReponseDataModel();
        }
    }

    public class OcrScanReponseDataModel
    {
        public OcrSyllabusResponseModel OcrSyllabusModel { get; set; }
        public IEnumerable<OcrAssignmentResponseModel> OcrAssignmentModel { get; set; }
        public bool Success { get; set; } = true;
    }

    public class OcrSyllabusResponseModel
    {
        public IEnumerable<TeacherNameModel> TeacherName { get; set; }
        public IEnumerable<ClassCodeModel> ClassCode { get; set; }
        public IEnumerable<ClassNameModel> ClassName { get; set; }
        public IEnumerable<ClassScheduleModel> ClassSchedule { get; set; }
    }

    public class OcrAssignmentResponseModel
    {
        public AssignmentTitleModel AssignmentTitle { get; set; }
        //public string Notes { get; set; }
        public AssignmentStartDateModel AssignmentDateStart { get; set; }
        public AssignmentEndDateModel AssignmentDateEnd { get; set; }
    }

    public class ConfidenceScoreModel
    {
        public float ConfidenceScore { get; set; }
    }

    public class ClassCodeModel : ConfidenceScoreModel
    {
        public string Name { get; set; }
    }

    public class ClassNameModel : ConfidenceScoreModel
    {
        public string Name { get; set; }
    }

    public class TeacherNameModel : ConfidenceScoreModel
    {
        public string Name { get; set; }
    }

    public class ClassScheduleModel : ConfidenceScoreModel
    {
        public string Name { get; set; }
    }

    public class AssignmentTitleModel : ConfidenceScoreModel
    {
        public string Name { get; set; }
    }

    public class AssignmentStartDateModel : ConfidenceScoreModel
    {
        public string Name { get; set; }
    }
    public class AssignmentEndDateModel : ConfidenceScoreModel
    {
        public string Name { get; set; }
    }

    public class TableDetailModel : ConfidenceScoreModel
    {
        public string TableId { get; set; }
        public bool IsHeader { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public string Value { get; set; }
        public IEnumerable<string> ChildIds { get; set; }

    }

    public class TableModel
    {
        public IEnumerable<TableDetailModel> TableDetails { get; set; }
    }
}
