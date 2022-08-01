using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface INotificationService
    {
        NotificationTokenResponseModel UpdateUserNotification(NotificationTokenRequestModel userRequestModel);
        NotificationTokenResponseModel SendNotification(SendNotificationRequestModel sendNotificationRequestModel);
    }
}
