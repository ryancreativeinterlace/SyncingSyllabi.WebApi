using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Interfaces
{
    public interface IUserService
    {
        UserDto GetUserById(long userId);
    }
}
