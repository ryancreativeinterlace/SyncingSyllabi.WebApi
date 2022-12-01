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

    public class AssignmentAttachmentResponseModel : BaseResponseModel<AssignmentAttachmentResponseDataModel>
    {
        public AssignmentAttachmentResponseModel()
        {
            Data = new AssignmentAttachmentResponseDataModel();
        }
    }

    public class AssignmentAttachmentResponseDataModel
    {
        public string AttachmentUrl { get; set; }
        public bool Success { get; set; } = true;
    }
}

