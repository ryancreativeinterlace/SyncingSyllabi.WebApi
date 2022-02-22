using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface IUserService
    {
        UserDto CreateUser(UserRequestModel userRequestModel);
        UserDto UpdateUser(UserRequestModel userRequestModel);
        UserDto GetActiveUserLogin(AuthRequestModel authRequestModel);
        UserDto GetUserById(long userId);
        UserDto GetUserByEmail(string email);
    }
}
