using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class UserDecryptPasswordResponseModel : BaseResponseModel<UserDecryptPasswordDataModel>
    {
        public UserDecryptPasswordResponseModel()
        {
            Data = new UserDecryptPasswordDataModel();
        }
    }

    public class UserDecryptPasswordDataModel
    {
        public string DecryptedPassword { get; set; }
    }

    public class AppleAuthResponseModel : BaseResponseModel<AppleAuthDataModel>
    {
        public AppleAuthResponseModel()
        {
            Data = new AppleAuthDataModel();
        }
    }

    public class AppleAuthDataModel
    {
        public string AppleToken { get; set; }
        public bool Success { get; set; } = true;
    }
}
