using Microsoft.EntityFrameworkCore;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using SyncingSyllabi.Contexts.Entities;
using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Data.Enums;

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
                             .Select(s => _mapper.Map<UserEntity>(s))
                             .FirstOrDefault();

                if (getUser == null)
                {
                    user.FillCreated(user.Id);
                    user.FillUpdated(user.Id);

                    ctx.Users.Add(user);

                    ctx.SaveChanges();

                    result = _mapper.Map<UserDto>(user);
                }
            });

            return result;
        }

        public UserDto UpdateUser(UserDto userDto)
        {
            UserDto result = null;

            var user = _mapper.Map<UserEntity>(userDto);

            UseDataContext(ctx =>
            {
                var getUser = ctx.Users
                             .AsNoTracking()
                             .Where(w => w.Id == user.Id)
                             .Select(s => _mapper.Map<UserEntity>(s))
                             .FirstOrDefault();

                if (getUser != null)
                {
                    getUser.FirstName = !string.IsNullOrEmpty(user.FirstName) ? user.FirstName.Trim() : getUser.FirstName;
                    getUser.LastName = !string.IsNullOrEmpty(user.LastName) ? user.LastName.Trim() : getUser.LastName;
                    getUser.Email = !string.IsNullOrEmpty(user.Email) ? user.Email.Trim() : getUser.Email;
                    getUser.School = !string.IsNullOrEmpty(user.School) ? user.School.Trim() : getUser.School;
                    getUser.Major = !string.IsNullOrEmpty(user.Major) ? user.Major.Trim() : getUser.Major;
                    getUser.Password = !string.IsNullOrEmpty(user.Password) ? user.Password : getUser.Password;
                    getUser.DateOfBirth = user.DateOfBirth ?? getUser.DateOfBirth;
                    getUser.IsActive = user.IsActive ?? getUser.IsActive;
                    getUser.ImageName = !string.IsNullOrEmpty(user.ImageName) ? user.ImageName : getUser.ImageName;
                    getUser.ImageUrl = !string.IsNullOrEmpty(user.ImageUrl) ? user.ImageUrl : getUser.ImageUrl;

                    getUser.FillCreated(getUser.Id);
                    getUser.FillUpdated(getUser.Id);

                    ctx.Users.Update(getUser);

                    ctx.SaveChanges();

                    result = _mapper.Map<UserDto>(getUser);
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
                                    w.IsActive.Value)
                             .Select(s => _mapper.Map<UserDto>(s))
                             .FirstOrDefault();
            });

            return result;
        }

        public UserDto UserLogin(string email, string password)
        {
            UserDto result = null;

            UseDataContext(ctx =>
            {
                result = ctx.Users
                             .AsNoTracking()
                             .Where(w =>
                                    w.Email.ToLower() == email.ToLower() &&
                                    w.Password == password)
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
                                    w.IsActive.Value)
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
                                    w.Email.ToLower() == email.ToLower())
                             .Select(s => _mapper.Map<UserDto>(s))
                             .FirstOrDefault();
            });

            return result;
        }

        public UserCodeDto CreateUserCode(UserCodeDto userCodeDto)
        {
            UserCodeDto result = null;

            var user = _mapper.Map<UserCodeEntity>(userCodeDto);

            user.CodeExpiration = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_syncingSyllabiSettings.UserCodeExpirationInMinutes));

            UseDataContext(ctx =>
            {
                var getUserCode = ctx.UserCodes
                                 .AsNoTracking()
                                 .Where(w => w.UserId == user.UserId)
                                 .Select(s => _mapper.Map<UserCodeEntity>(s))
                                 .FirstOrDefault();

                if (getUserCode == null)
                {
                    user.FillCreated(user.Id);
                    user.FillUpdated(user.Id);

                    ctx.UserCodes.Add(user);

                    ctx.SaveChanges();

                    result = _mapper.Map<UserCodeDto>(user);
                }
            });

            return result;
        }

        public UserCodeDto GetUserCode(long userId, CodeTypeEnum codeType)
        {
            UserCodeDto result = null;

            UseDataContext(ctx =>
            {
                result = ctx.UserCodes
                             .AsNoTracking()
                             .Where(w =>
                                    w.UserId == userId &&
                                    w.CodeType == codeType &&
                                    w.IsActive.Value)
                             .Select(s => _mapper.Map<UserCodeDto>(s))
                             .FirstOrDefault();



            });

            return result;
        }

        public UserCodeDto UpdateUserCode(UserCodeDto userCodeDto)
        {
            UserCodeDto result = null;

            var userCode = _mapper.Map<UserCodeEntity>(userCodeDto);

            UseDataContext(ctx =>
            {
                var getUserCode = ctx.UserCodes
                                 .AsNoTracking()
                                 .Where(w =>
                                        w.UserId == userCode.UserId &&
                                        w.CodeType == userCode.CodeType &&
                                        w.IsActive.Value)
                                 .Select(s => _mapper.Map<UserCodeEntity>(s))
                                 .FirstOrDefault();

                if (getUserCode != null)
                {

                    getUserCode.VerificationCode = !string.IsNullOrEmpty(userCode.VerificationCode) ? userCode.VerificationCode : getUserCode.VerificationCode;
                    getUserCode.CodeType = userCode.CodeType != 0 ? userCode.CodeType : getUserCode.CodeType;
                    getUserCode.CodeTypeName = !string.IsNullOrEmpty(userCode.CodeTypeName) ? userCode.CodeTypeName : getUserCode.CodeTypeName;
                    getUserCode.CodeExpiration = userCode.CodeExpiration ?? getUserCode.CodeExpiration;
                    getUserCode.IsActive = userCode.IsActive ?? getUserCode.IsActive;

                    getUserCode.FillCreated(getUserCode.Id);
                    getUserCode.FillUpdated(getUserCode.Id);

                    ctx.UserCodes.Update(getUserCode);

                    ctx.SaveChanges();

                    result = _mapper.Map<UserCodeDto>(getUserCode);
                }
            });

            return result;
        }
    }
}
