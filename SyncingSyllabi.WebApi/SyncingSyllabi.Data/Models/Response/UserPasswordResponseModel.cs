using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class UserPasswordResponseModel : BaseResponseModel<UserPassowrdResponseDataModel>
    {
        public UserPasswordResponseModel()
        {
            Data = new UserPassowrdResponseDataModel();
        }
    }

    public class UserPassowrdResponseDataModel
    {
        public bool Success { get; set; } = true;
    }
}
