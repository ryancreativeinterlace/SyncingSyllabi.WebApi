using SyncingSyllabi.Data.Models.Base;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class AssignmentResponseModel : BaseResponseModel<AssignmentResponseDataModel>
    {
        public AssignmentResponseModel()
        {
            Data = new AssignmentResponseDataModel();
        }
    }

    public class AssignmentResponseDataModel
    {
        public AssignmentModel Item { get; set; }
        public bool Success { get; set; } = true;
    }
}

