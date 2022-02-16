using SyncingSyllabi.Data.Models.Base;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class AuthResponseModel : BaseResponseModel<AuthResponseDataModel>
    {
        public AuthResponseModel()
        {
            Data = new AuthResponseDataModel();
        }
    }

    public class AuthResponseDataModel
    {
        public AuthModel Item { get; set; }
        public bool Success { get; set; } = true;
    }
}
