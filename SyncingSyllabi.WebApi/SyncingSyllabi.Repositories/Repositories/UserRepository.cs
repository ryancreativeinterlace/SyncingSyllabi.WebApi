using Microsoft.EntityFrameworkCore;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using SyncingSyllabi.Contexts.Entities;

namespace SyncingSyllabi.Repositories.Repositories
{
    public partial class UserBaseRepository
    {
        public UserDto CreateUser(UserDto userDto)
        {
            UserDto result = null;

            var user = _mapper.Map<UserEntity>(userDto);

            UseDataContext(ctx =>
            {
                var getUser = ctx.Users
                             .AsNoTracking()
                             .Where(w => w.Email.ToLower() == user.Email.ToLower())
                             .Select(s => _mapper.Map<UserDto>(s))
                             .FirstOrDefault();

                if (getUser == null)
                {
                    ctx.Users.Add(user);

                    user.FillCreated(user.Id);
                    user.FillUpdated(user.Id);

                    ctx.SaveChanges();

                    result = _mapper.Map<UserDto>(user);
                }
            });

            return result;
        }

        public UserDto GetActiveUserLogin(string email, string password)
        {
            UserDto result = null;

            UseDataContext(ctx =>
            {
                result = ctx.Users
                             .AsNoTracking()
                             .Where(w => 
                                    w.Email.ToLower() == email.ToLower() &&
                                    w.Password == password &&
                                    w.Active)
                             .Select(s => _mapper.Map<UserDto>(s))
                             .FirstOrDefault();
            });

            return result;
        }

        public UserDto GetUserById(long userId)
        {
            UserDto result = null;

            UseDataContext(ctx =>
            {
                result = ctx.Users
                             .AsNoTracking()
                             .Where(w => 
                                    w.Id == userId &&
                                    w.Active)
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
                             .Where(w => 
                                    w.Email.ToLower() == email.ToLower() &&
                                    w.Active)
                             .Select(s => _mapper.Map<UserDto>(s))
                             .FirstOrDefault();
            });

            return result;
        }
    }
}
