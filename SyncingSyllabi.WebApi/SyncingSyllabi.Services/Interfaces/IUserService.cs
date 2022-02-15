using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface IUserService
    {
        UserDto CreateUser(UserModel userModel);
        UserDto GetUserById(long userId);
        UserDto GetUserByEmail(string email);
    }
}
