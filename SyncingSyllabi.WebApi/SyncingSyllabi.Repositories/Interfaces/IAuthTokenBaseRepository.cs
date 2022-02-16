using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Repositories.Interfaces
{
    public interface IAuthTokenBaseRepository
    {
        AuthTokenDto CreateAuthToken(AuthTokenDto authTokenDto);
        AuthTokenDto GetAuthToken(long userId);
    }
}
