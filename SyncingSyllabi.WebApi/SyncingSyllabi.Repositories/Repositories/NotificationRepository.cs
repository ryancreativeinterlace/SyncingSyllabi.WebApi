using SyncingSyllabi.Common.Tools.Extensions;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class NotificationBaseRepository
    {
        public UserNotificationDto CreateUserNotification(UserNotificationDto userNotificationDto)
        {
            UserNotificationDto result = null;

            var userNotification = _mapper.Map<UserNotificationEntity>(userNotificationDto);

            UseDataContext(ctx =>
            {
                var getUser = ctx.UserNotifications
                             .AsNoTracking()
                             .Where(w => w.Id == userNotification.Id)
                             .Select(s => _mapper.Map<UserNotificationEntity>(s))
                             .FirstOrDefault();

                if (getUser == null)
                {
                    userNotification.FillCreated(userNotification.UserId);
                    userNotification.FillUpdated(userNotification.UserId);

                    ctx.UserNotifications.Add(userNotification);

                    ctx.SaveChanges();

                    result = _mapper.Map<UserNotificationDto>(userNotification);
                }
            });

            return result;
        }

        public UserNotificationListResponseModel GetUserNoficationList(long userId, UserNotificationStatusEnum userNotificationStatusEnum, PaginationDto pagination)
        {
            var result = new UserNotificationListResponseModel();

            var errorList = new List<string>();

            var getUserNotificationList = new List<UserNotificationDto>();

            IEnumerable<UserNotificationModel> getUserNotificationListResult = Enumerable.Empty<UserNotificationModel>();

            UseDataContext(ctx =>
            {
                switch(userNotificationStatusEnum)
                {
                    case UserNotificationStatusEnum.All:

                        getUserNotificationList = ctx.UserNotifications
                                                        .AsNoTracking()
                                                        .Where(w => w.UserId == userId &&
                                                               w.IsActive.Value)
                                                        .Select(s => _mapper.Map<UserNotificationDto>(s))
                                                        .ToList();

                    break;

                    case UserNotificationStatusEnum.NotYetRead:

                        getUserNotificationList = ctx.UserNotifications
                                                        .AsNoTracking()
                                                        .Where(w => w.UserId == userId &&
                                                               !w.IsRead &&
                                                               w.IsActive.Value)
                                                        .Select(s => _mapper.Map<UserNotificationDto>(s))
                                                        .ToList();
                    break;

                    case UserNotificationStatusEnum.Read:

                        getUserNotificationList = ctx.UserNotifications
                                                        .AsNoTracking()
                                                        .Where(w => w.UserId == userId &&
                                                               w.IsRead &&
                                                               w.IsActive.Value)
                                                        .Select(s => _mapper.Map<UserNotificationDto>(s))
                                                        .ToList();

                    break;
                }

                if (getUserNotificationList.Count() > 0)
                {
                    getUserNotificationListResult = _mapper.Map<IEnumerable<UserNotificationModel>>(getUserNotificationList);

                    if (getUserNotificationListResult.Count() > 0)
                    {
                        result.Data = getUserNotificationListResult.Page(pagination);
                    }
                }
                else
                {
                    errorList.Add("No result");
                }

                if (errorList.Count > 0)
                {
                    result.Errors = errorList;
                    result.Data.Success = false;
                }

            });

            return result;
        }
    }
}
