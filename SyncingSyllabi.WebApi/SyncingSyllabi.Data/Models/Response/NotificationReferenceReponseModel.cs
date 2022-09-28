using SyncingSyllabi.Data.Models.Base;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Data.Models.Response
{
    public class NotificationReferenceReponseModel : BaseResponseModel<NotificationReferenceDataModel>
    {
        public NotificationReferenceReponseModel()
        {
            Data = new NotificationReferenceDataModel();
        }
    }

    public class NotificationReferenceDataModel
    {
        public UserNotificationModel NotificationItem { get; set; }
        public bool Success { get; set; } = true;
    }

}
