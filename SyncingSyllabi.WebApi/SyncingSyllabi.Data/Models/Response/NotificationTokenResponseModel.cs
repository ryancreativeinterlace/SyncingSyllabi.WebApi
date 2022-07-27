﻿using SyncingSyllabi.Data.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class NotificationTokenResponseModel : BaseResponseModel<NotificationTokenResponseDataModel>
    {
        public NotificationTokenResponseModel()
        {
            Data = new NotificationTokenResponseDataModel();
        }
    }

    public class NotificationTokenResponseDataModel
    {
        public bool Success { get; set; } = true;
    }
}
