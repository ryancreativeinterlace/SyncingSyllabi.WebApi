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
        public SyllabusModel Item { get; set; }
        public bool Success { get; set; } = true;
    }
}
