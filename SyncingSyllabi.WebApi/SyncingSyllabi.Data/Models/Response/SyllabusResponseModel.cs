using SyncingSyllabi.Data.Models.Base;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class SyllabusResponseModel : BaseResponseModel<SyllabusResponseDataModel>
    {
        public SyllabusResponseModel()
        {
            Data = new SyllabusResponseDataModel();
        }
    }

    public class SyllabusResponseDataModel
    {
        public SyllabusDataOutputModel Item { get; set; }
        public bool Success { get; set; } = true;
    }

    public class SyllabusDataOutputModel : BaseTrackedModel
    {
        public Int64 UserId { get; set; }
        public string ClassCode { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public IEnumerable<string> ClassSchedule { get; set; }
        public string ColorInHex { get; set; }
    }
}
