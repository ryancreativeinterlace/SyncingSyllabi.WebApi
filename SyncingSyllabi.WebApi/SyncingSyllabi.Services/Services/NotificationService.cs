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

        public NotificationService
        (
            IMapper mapper,
            IUserBaseRepository userBaseRepository
        )
        {
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
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
    }
}
