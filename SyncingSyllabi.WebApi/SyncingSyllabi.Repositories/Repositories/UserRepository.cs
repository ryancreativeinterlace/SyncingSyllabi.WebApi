using Microsoft.EntityFrameworkCore;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class UserBaseRepository
    {
        public UserDto GetUserById(long userId)
        {
            UserDto result = null;

            UseDataContext(ctx =>
            {
                result = ctx.Users
                             .AsNoTracking()
                             .Where(w => w.Id == userId)
                             .Select(s => _mapper.Map<UserDto>(s))
                             .FirstOrDefault();
            });

            return result;
        }

        public UserDto GetUserByEmail(string email)
        {
            UserDto result = null;

            UseDataContext(ctx =>
            {
                result = ctx.Users
                             .AsNoTracking()
                             .Where(w => w.Email.ToLower() == email.ToLower())
                             .Select(s => _mapper.Map<UserDto>(s))
                             .FirstOrDefault();
            });

            return result;
        }
    }
}
