using SyncingSyllabi.Data.Models.Base;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{

    public class UserResponseModel : BaseResponseModel<UserResponseDataModel>
    {
        public UserResponseModel()
        {
            Data = new UserResponseDataModel();
        }
    }

    public class UserResponseDataModel
    {
        public UserModel Item { get; set; }
        public bool Success { get; set; } = true;
    }
}
