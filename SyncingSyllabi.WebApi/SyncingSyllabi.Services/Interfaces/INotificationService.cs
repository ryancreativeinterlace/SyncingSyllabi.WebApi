using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface INotificationService
    {
        NotificationTokenResponseModel UpdateUserNotification(NotificationTokenRequestModel userRequestModel);
        Task<NotificationTokenResponseModel> SendNotification(SendNotificationRequestModel sendNotificationRequestModel);
        UserNotificationListResponseModel GetUserNotificationList(UserNotificationListRequestModel userNotificationListRequestModel);
        NotificationTokenResponseModel ReadNotification(long notificationId);
    }
}
