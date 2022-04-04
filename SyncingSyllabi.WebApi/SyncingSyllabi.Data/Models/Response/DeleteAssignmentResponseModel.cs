using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class DeleteAssignmentResponseModel : BaseResponseModel<DeleteAssignmentDataModel>
    {
        public DeleteAssignmentResponseModel()
        {
            Data = new DeleteAssignmentDataModel();
        }
    }

    public class DeleteAssignmentDataModel
    {
        public bool Success { get; set; } = true;
    }
}
