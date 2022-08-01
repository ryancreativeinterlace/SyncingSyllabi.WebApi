using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
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
    }
}
