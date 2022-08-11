using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface INotificationBaseRepository
    {
        UserNotificationDto CreateUserNotification(UserNotificationDto userNotificationDto);
        UserNotificationListResponseModel GetUserNoficationList(long userId, UserNotificationStatusEnum userNotificationStatusEnum, PaginationDto pagination);
        UserNotificationDto GetUserNoficaitonById(long noficaitionId);
        UserNotificationDto UpdateNofication(UserNotificationDto userNotificationDto);
    }
}
