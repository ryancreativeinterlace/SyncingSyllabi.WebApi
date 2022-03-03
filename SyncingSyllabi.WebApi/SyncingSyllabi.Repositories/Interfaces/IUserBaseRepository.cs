using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IUserBaseRepository
    {
        UserDto CreateUser(UserDto userDto);
        UserDto UpdateUser(UserDto userDto);
        UserDto GetActiveUserLogin(string email, string password);
        UserDto GetUserById(long userId);
        UserDto GetUserByEmail(string email);
        UserCodeDto CreateUserCode(UserCodeDto userCodeDto);
        UserCodeDto GetUserCode(long userId, CodeTypeEnum codeType);
        UserCodeDto UpdateUserCode(UserCodeDto userCodeDto);
    }
}
