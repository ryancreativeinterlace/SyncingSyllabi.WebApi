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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBaseRepository;
        private readonly IGoalBaseRepository _goalBaseRepository;
        private readonly IAssignmentBaseRepository _assignmentBaseRepository;
        private readonly INotificationBaseRepository _notificationBaseRepository;

        public NotificationService
        (
            IMapper mapper,
            IUserBaseRepository userBaseRepository,
            IGoalBaseRepository goalBaseRepository,
            IAssignmentBaseRepository assignmentBaseRepository,
            INotificationBaseRepository notificationBaseRepository
        )
        {
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
            _goalBaseRepository = goalBaseRepository;
            _assignmentBaseRepository = assignmentBaseRepository;
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
            sendNoty.ReferenceId = sendNotificationRequestModel.ReferenceId;
            sendNoty.IsActive = true;
            sendNoty.IsRead = false;

            switch(sendNotificationRequestModel.NotificationType)
            {
                case NotificationTypeEnum.Assignment:
                    {
                        sendNoty.NotificationType = NotificationTypeEnum.Assignment;
                        sendNoty.NotificationTypeName = NotificationTypeEnum.Assignment.ToString();

                        break;
                    }

                case NotificationTypeEnum.Goal:
                    {
                        sendNoty.NotificationType = NotificationTypeEnum.Goal;
                        sendNoty.NotificationTypeName = NotificationTypeEnum.Goal.ToString();

                        break;
                    }
            }

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
            var sortColumnDto = userNotificationListRequestModel.Sort?.Select(f => _mapper.Map<SortColumnDto>(f));
            var getUserNoficationList = _notificationBaseRepository.GetUserNoficationList(userNotificationListRequestModel.UserId, userNotificationListRequestModel.UserNotificationStatus, sortColumnDto, paginationDto);

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

                if (readNotifcation == null)
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

        public NotificationTokenResponseModel RemoveNotification(long notificationId)
        {
            var errorList = new List<string>();

            var readNotificationResult = new NotificationTokenResponseModel();

            var getNotification = _notificationBaseRepository.GetUserNoficaitonById(notificationId);

            if (getNotification != null)
            {
                getNotification.IsActive = false;

                var readNotifcation = _notificationBaseRepository.UpdateNofication(getNotification);

                if (readNotifcation == null)
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

        public async Task<NotificationDueDateResponseModel> GetGoalDueDate(DateTime dateTime)
        {
            var errorList = new List<string>();

            var goalDueDate = new NotificationDueDateResponseModel();

            var getGoalsDue = _goalBaseRepository.GetDueGoals(dateTime.AddDays(1));

            if (getGoalsDue.Count() > 0)
            {
                foreach (var dueGoal in getGoalsDue)
                {
                    var sendNoty = new SendNotificationRequestModel();

                    sendNoty.UserId = dueGoal.UserId;
                    sendNoty.Title = $"Goal {dueGoal.GoalTitle} is almost done";
                    sendNoty.Message = $"{dueGoal.GoalDateEnd.Value.ToShortDateString()} | {dueGoal.GoalDateEnd.Value.ToString("hh:mm tt")}";

                    await this.SendNotification(sendNoty);
                }

                goalDueDate.Data.HasDueNotification = true;
            }

            return goalDueDate;
        }

        public async Task<NotificationDueDateResponseModel> GetAssignmentDueDate(DateTime dateTime)
        {
            var errorList = new List<string>();

            var assignmentDueDate = new NotificationDueDateResponseModel();

            var getAssignmentsDue = _assignmentBaseRepository.GetDueAssignments(dateTime.AddDays(1));

            if (getAssignmentsDue.Count() > 0)
            {
                foreach (var dueAssignment in getAssignmentsDue)
                {
                    var sendNoty = new SendNotificationRequestModel();

                    sendNoty.UserId = dueAssignment.UserId;
                    sendNoty.Title = $"Assignment {dueAssignment.AssignmentTitle} is due tommorow";
                    sendNoty.Message = $"Due Date: {dueAssignment.AssignmentDateEnd.Value.ToShortDateString()} | {dueAssignment.AssignmentDateEnd.Value.ToString("hh:mm tt")}";

                    await this.SendNotification(sendNoty);
                }

                assignmentDueDate.Data.HasDueNotification = true;
            }

            return assignmentDueDate;
        }

        public async Task<NotificationDueDateResponseModel> GetDues(DateTime dateTime)
        {
            var errorList = new List<string>();

            var dues = new NotificationDueDateResponseModel();
            var assingmentIds = new List<Int64>();
            var goalIds = new List<Int64>();

            var getAssignmentsDue = _assignmentBaseRepository.GetDueAssignments(dateTime.AddDays(1));
            var getGoalsDue = _goalBaseRepository.GetDueGoals(dateTime.AddDays(1));

            if (getAssignmentsDue.Count() > 0)
            {
                foreach (var dueAssignment in getAssignmentsDue)
                {
                    var reference = this.GetReferenceDetails(dueAssignment.Id);

                    if(reference.Data.NotificationItem == null)
                    {
                        var sendNoty = new SendNotificationRequestModel();

                        sendNoty.UserId = dueAssignment.UserId;
                        sendNoty.Title = $"Assignment {dueAssignment.AssignmentTitle} is due tommorow";
                        sendNoty.Message = $"Due Date: {dueAssignment.AssignmentDateEnd.Value.ToShortDateString()} | {dueAssignment.AssignmentDateEnd.Value.ToString("hh:mm tt")}";
                        sendNoty.NotificationType = NotificationTypeEnum.Assignment;
                        sendNoty.ReferenceId = dueAssignment.Id;

                        await this.SendNotification(sendNoty);

                        assingmentIds.Add(dueAssignment.Id);
                    }
                }
            }

            if (getGoalsDue.Count() > 0)
            {
                foreach (var dueGoal in getGoalsDue)
                {
                    var reference = this.GetReferenceDetails(dueGoal.Id);

                    if(reference.Data.NotificationItem == null)
                    {
                        var sendNoty = new SendNotificationRequestModel();

                        sendNoty.UserId = dueGoal.UserId;
                        sendNoty.Title = $"Goal {dueGoal.GoalTitle} is almost done";
                        sendNoty.Message = $"{dueGoal.GoalDateEnd.Value.ToShortDateString()} | {dueGoal.GoalDateEnd.Value.ToString("hh:mm tt")}";
                        sendNoty.NotificationType = NotificationTypeEnum.Goal;
                        sendNoty.ReferenceId = dueGoal.Id;

                        await this.SendNotification(sendNoty);

                        goalIds.Add(dueGoal.Id);
                    }
                }
            }

            if(assingmentIds.Count() > 0 || goalIds.Count() > 0)
            {
                dues.Data.AssignmentIds = assingmentIds;
                dues.Data.GoalIds = goalIds;
                dues.Data.HasDueNotification = true;
            }

            return dues;
        }

        public NotificationReferenceReponseModel GetReferenceDetails(long referenceId)
        {
            var referenceResult = new NotificationReferenceReponseModel();

            var getNotification = _notificationBaseRepository.GetUserNoficaitonByReferenceId(referenceId);

            if (getNotification != null)
            {
                referenceResult.Data.NotificationItem = _mapper.Map<UserNotificationModel>(getNotification);
            }

            if(getNotification == null)
            {
                referenceResult.Data.Success = false;
            }

            return referenceResult;
        }
    }
}
