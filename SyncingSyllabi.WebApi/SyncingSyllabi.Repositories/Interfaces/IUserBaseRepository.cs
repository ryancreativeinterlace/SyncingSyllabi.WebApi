using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IUserBaseRepository
    {
        UserDto CreateUser(UserDto userDto);
        UserDto GetUserById(long userId);
        UserDto GetUserByEmail(string email);
    }
}
