using AutoMapper;
using FirebaseAdmin.Messaging;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
                updateNotificationToken = _userBaseRepository.UpdateUserNotificationToken(user);

            }

            return updateNotificationToken;
        }

        public async Task<NotificationTokenResponseModel> SendNotification(SendNotificationRequestModel sendNotificationRequestModel)
        {
            var errorList = new List<string>();

            var sendNotification = new NotificationTokenResponseModel();

            var sendNoty = new UserNotificationModel();

            sendNoty.UserId = sendNotificationRequestModel.UserId;
            sendNoty.Title = sendNotificationRequestModel.Title;
            sendNoty.Message = sendNotificationRequestModel.Message;
            sendNoty.IsActive = true;
            sendNoty.IsRead = false;

            var getUser = _userBaseRepository.GetUserById(sendNoty.UserId);

            if (getUser != null && !string.IsNullOrEmpty(getUser.NotificationToken))
            {
                // Send Message
                var message = new Message()
                {
                    Notification = new Notification
                    {
                        Title = sendNoty.Title,
                        Body = sendNoty.Message,

                    },
                    Token = getUser.NotificationToken,
                };

                var messaging = FirebaseMessaging.DefaultInstance;

                var send = await messaging.SendAsync(message);

                if (send != null)
                {
                    sendNoty.NotificationStatus = NotificationStatusEnum.Success;
                    sendNoty.NotificationStatusName = NotificationStatusEnum.Success.ToString();
                }
                else
                {
                    sendNoty.NotificationStatus = NotificationStatusEnum.Failed;
                    sendNoty.NotificationStatusName = NotificationStatusEnum.Failed.ToString();
                }
            }
            else
            {
                errorList.Add("User or notification token don't exist.");
                sendNotification.Errors = errorList;
                sendNotification.Data.Success = false;
            }
            var userNoty = _mapper.Map<UserNotificationDto>(sendNoty);

            // Save noty on database
            var createNoty = _notificationBaseRepository.CreateUserNotification(userNoty);

            return sendNotification;
        }

        public UserNotificationListResponseModel GetUserNotificationList(UserNotificationListRequestModel userNotificationListRequestModel)
        {
            var paginationDto = userNotificationListRequestModel.Pagination != null ? _mapper.Map<PaginationDto>(userNotificationListRequestModel.Pagination) : null;

            var getUserNoficationList = _notificationBaseRepository.GetUserNoficationList(userNotificationListRequestModel.UserId, userNotificationListRequestModel.UserNotificationStatus, paginationDto);

            return getUserNoficationList;
        }

        public NotificationTokenResponseModel ReadNotification(long notificationId)
        {
            var errorList = new List<string>();

            var readNotificationResult = new NotificationTokenResponseModel();

            var getNotification = _notificationBaseRepository.GetUserNoficaitonById(notificationId);

            if (getNotification != null)
            {
                getNotification.IsRead = true;

                var readNotifcation = _notificationBaseRepository.UpdateNofication(getNotification);

                if(readNotifcation == null)
                {
                    errorList.Add("Notificaation can't be updated.");
                    readNotificationResult.Errors = errorList;
                    readNotificationResult.Data.Success = false;
                }
            }
            else
            {
                errorList.Add("Notificaation don't exist.");
                readNotificationResult.Errors = errorList;
                readNotificationResult.Data.Success = false;
            }

            return readNotificationResult;
        }
    }
}
