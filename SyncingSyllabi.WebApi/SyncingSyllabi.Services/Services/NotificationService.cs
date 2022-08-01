using AutoMapper;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBaseRepository;
        private readonly INotificationBaseRepository _notificationBaseRepository;

        public NotificationService
        (
            IMapper mapper,
            IUserBaseRepository userBaseRepository,
            INotificationBaseRepository notificationBaseRepository
        )
        {
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
            _notificationBaseRepository = notificationBaseRepository;
        }

        public NotificationTokenResponseModel UpdateUserNotification(NotificationTokenRequestModel userRequestModel)
        {
            NotificationTokenResponseModel updateNotificationToken = null;

            var userModel = new UserModel();

            userModel.Id = userRequestModel.UserId;
            userModel.NotificationToken = userRequestModel.NotificationToken;

            UserDto user = _mapper.Map<UserDto>(userModel);

            if (user != null)
            {
                updateNotificationToken = _userBaseRepository.UpdateUserNotification(user);

            }

            return updateNotificationToken;
        }

        public NotificationTokenResponseModel SendNotification(SendNotificationRequestModel sendNotificationRequestModel)
        {
            NotificationTokenResponseModel sendNotification = null;

            var sendNoty = new UserNotificationModel();

            sendNoty.UserId = sendNotificationRequestModel.UserId;
            sendNoty.Title = sendNotificationRequestModel.Title;
            sendNoty.Message = sendNotificationRequestModel.Message;
            sendNoty.IsActive = true;

            UserNotificationDto userNoty = _mapper.Map<UserNotificationDto>(sendNoty);

            if(userNoty != null)
            {
                var createNoty = _notificationBaseRepository.CreateUserNotification(userNoty);
            }

            return sendNotification;
        }
    }
}
