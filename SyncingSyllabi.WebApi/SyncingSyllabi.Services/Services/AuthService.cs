using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SyncingSyllabi.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserBaseRepository _userBaseRepository;
        public AuthService
        (
            IUserBaseRepository userBaseRepository
        )
        {
            _userBaseRepository = userBaseRepository;
        }

        public AuthTokenDto GetAuthToken (AuthRequestModel authRequestModel)
        {
            AuthTokenDto authTokenResult = null;

            var getUser = _userBaseRepository.GetActiveUserLogin(authRequestModel.Email, PasswordUtility.EncryptPassword(authRequestModel.Password));

            if(getUser != null)
            {

            }

            return authTokenResult;
        }
    }
}
