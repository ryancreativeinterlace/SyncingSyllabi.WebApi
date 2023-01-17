using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IUserBaseRepository
    {
        UserDto CreateUser(UserDto userDto);
        UserDto UpdateUser(UserDto userDto);
        UserDto GetActiveUserLogin(string email, string password, bool isGoogle);
        UserDto UserLogin(string email, string password, bool isGoogle);
        UserDto GetUserById(long userId);
        UserDto GetUserByEmail(string email);
        UserCodeDto CreateUserCode(UserCodeDto userCodeDto);
        UserCodeDto GetUserCode(long userId, CodeTypeEnum codeType);
        UserCodeDto UpdateUserCode(UserCodeDto userCodeDto);
        void CreateUserEmailTracks(IEnumerable<UserEmailTrackingDto> userEmailTrackingDtos);
        NotificationTokenResponseModel UpdateUserNotificationToken(UserDto userDto);
        bool HardDeleteUser(string email);
    }
}
