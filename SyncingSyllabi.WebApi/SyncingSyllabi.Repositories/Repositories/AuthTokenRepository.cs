using Microsoft.EntityFrameworkCore;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Data.Dtos.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class AuthTokenBaseRepository
    {
        public AuthTokenDto CreateAuthToken(AuthTokenDto authTokenDto)
        {
            AuthTokenDto authTokenResult = null;

            var authToken = _mapper.Map<AuthTokenEntity>(authTokenDto);

            UseDataContext(ctx =>
            {
                var getAuth = ctx.AuthTokens
                             .AsNoTracking()
                             .Where(w => w.UserId == authTokenDto.UserId)
                             .Select(s => _mapper.Map<AuthTokenDto>(s))
                             .FirstOrDefault();

                if (getAuth == null)
                {
                    ctx.AuthTokens.Add(authToken);

                    authToken.FillCreated(authTokenDto.UserId);
                    authToken.FillUpdated(authTokenDto.UserId);

                    ctx.SaveChanges();

                    authTokenResult = _mapper.Map<AuthTokenDto>(authTokenDto);
                }
            });

            return authTokenResult;
        }

        public AuthTokenDto UpdateAuthToken(AuthTokenDto authTokenDto)
        {
            AuthTokenDto authTokenResult = null;

            var authToken = _mapper.Map<AuthTokenEntity>(authTokenDto);

            UseDataContext(ctx =>
            {
                var getAuth = ctx.AuthTokens
                             .AsNoTracking()
                             .Where(w => w.UserId == authTokenDto.UserId)
                             .Select(s => _mapper.Map<AuthTokenDto>(s))
                             .FirstOrDefault();

                if (getAuth != null)
                {
                    var updateAuth = _mapper.Map<AuthTokenEntity>(getAuth);

                    ctx.AuthTokens.Update(updateAuth);

                    authToken.FillCreated(getAuth.UserId);
                    authToken.FillUpdated(getAuth.UserId);

                    ctx.SaveChanges();

                    authTokenResult = _mapper.Map<AuthTokenDto>(authTokenDto);
                }
            });

            return authTokenResult;
        }

        public AuthTokenDto GetAuthToken(long userId)
        {
            AuthTokenDto authTokenResult = null;

            UseDataContext(ctx =>
            {
                var getAuth = ctx.AuthTokens
                             .AsNoTracking()
                             .Where(w => w.UserId == userId)
                             .Select(s => _mapper.Map<AuthTokenDto>(s))
                             .FirstOrDefault();

                authTokenResult = _mapper.Map<AuthTokenDto>(getAuth);
            });

            return authTokenResult;
        }
    }
}
