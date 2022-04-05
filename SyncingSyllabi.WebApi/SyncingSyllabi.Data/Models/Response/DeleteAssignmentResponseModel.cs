using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class DeleteResponseModel : BaseResponseModel<DeleteDataModel>
    {
        public DeleteResponseModel()
        {
            Data = new DeleteDataModel();
        }
    }

    public class DeleteDataModel
    {
        public bool Success { get; set; } = true;
    }
}
