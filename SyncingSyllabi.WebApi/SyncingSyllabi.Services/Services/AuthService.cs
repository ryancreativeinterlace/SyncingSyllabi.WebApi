using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Settings;
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
        private readonly IAuthTokenBaseRepository _authTokenBaseRepository;
        AuthSettings _authSettings;

        public AuthService
        (
            IUserBaseRepository userBaseRepository,
            IAuthTokenBaseRepository authTokenBaseRepository,
            AuthSettings authSettings
        )
        {
            _userBaseRepository = userBaseRepository;
            _authTokenBaseRepository = authTokenBaseRepository;
            _authSettings = authSettings;
        }

        public AuthTokenDto GetAuthToken (AuthRequestModel authRequestModel)
        {
            AuthTokenDto authTokenResult = null;

            var getUser = _userBaseRepository.GetActiveUserLogin(authRequestModel.Email, EncryptionUtility.EncryptString(authRequestModel.Password));

            if(getUser != null)
            {
                var getAuth = _authTokenBaseRepository.GetAuthToken(getUser.Id);

                if(getAuth != null)
                {
                    if(getAuth.AuthTokenExpiration.Value > DateTime.Now && getAuth.Active)
                    {
                        // Refresh Token
                        var refreshAuth = new AuthTokenDto()
                        {
                            UserId = getUser.Id,
                            AuthToken = TokenUtility.GenerateAccessToken(getUser.Id, getUser.Email, $"{getUser.FirstName} {getUser.LastName}"),
                            AuthTokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(_authSettings.ExpirationInMinutes)),
                            Active = true
                        };

                        authTokenResult = _authTokenBaseRepository.UpdateAuthToken(refreshAuth);
                    }
                }
                else
                {
                    // Generate Token
                    var newAuth = new AuthTokenDto()
                    {
                        UserId = getUser.Id,
                        AuthToken = TokenUtility.GenerateAccessToken(getUser.Id, getUser.Email, $"{getUser.FirstName} {getUser.LastName}"),
                        AuthTokenExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(_authSettings.ExpirationInMinutes)),
                        Active = true
                    };

                    authTokenResult = _authTokenBaseRepository.CreateAuthToken(newAuth);
                }
            }

            return authTokenResult;
        }
    }
}
