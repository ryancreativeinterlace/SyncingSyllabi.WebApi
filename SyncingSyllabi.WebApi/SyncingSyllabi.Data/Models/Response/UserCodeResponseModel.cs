using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class UserCodeResponseModel : BaseResponseModel<UserCodeResponseDataModel>
    {
        public UserCodeResponseModel()
        {
            Data = new UserCodeResponseDataModel();
        }
    }

    public class UserCodeResponseDataModel
    {
        public bool Success { get; set; } = true;
    }
}
