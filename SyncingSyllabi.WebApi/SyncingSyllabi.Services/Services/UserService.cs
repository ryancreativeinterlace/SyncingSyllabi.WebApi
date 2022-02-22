using AutoMapper;
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

    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBaseRepository;

        public UserService
        (
            IMapper mapper,
            IUserBaseRepository userBaseRepository
        )
        {
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
        }

        public UserDto CreateUser(UserRequestModel userRequestModel)
        {
            var userModel = new UserModel();

            userModel.FirstName = !string.IsNullOrEmpty(userRequestModel.FirstName) ? userRequestModel.FirstName.Trim() : string.Empty;
            userModel.LastName = !string.IsNullOrEmpty(userRequestModel.LastName) ? userRequestModel.LastName.Trim() : string.Empty;
            userModel.Email = !string.IsNullOrEmpty(userRequestModel.Email) ? userRequestModel.Email.Trim() : string.Empty;
            userModel.School = !string.IsNullOrEmpty(userRequestModel.School) ? userRequestModel.School.Trim() : string.Empty;
            userModel.Major = !string.IsNullOrEmpty(userRequestModel.Major) ? userRequestModel.Major.Trim() : string.Empty;
            userModel.DateOfBirth = userRequestModel.DateOfBirth ?? null;
            userModel.Password = EncryptionUtility.EncryptString(userRequestModel.Password.Trim());
            userModel.IsActive = true;

            UserDto createUserResult = null;
            UserDto user = _mapper.Map<UserDto>(userModel);
            
            if(user != null)
            {
                createUserResult = _userBaseRepository.CreateUser(user);
            }

            return createUserResult;
        }

        public UserDto UpdateUser(UserRequestModel userRequestModel)
        {
            var userModel = new UserModel();

            userModel.Id = userRequestModel.UserId;
            userModel.FirstName = !string.IsNullOrEmpty(userRequestModel.FirstName) ? userRequestModel.FirstName.Trim() : string.Empty;
            userModel.LastName = !string.IsNullOrEmpty(userRequestModel.LastName) ? userRequestModel.LastName.Trim() : string.Empty;
            userModel.Email = !string.IsNullOrEmpty(userRequestModel.Email) ? userRequestModel.Email.Trim() : string.Empty;
            userModel.School = !string.IsNullOrEmpty(userRequestModel.School) ? userRequestModel.School.Trim() : string.Empty;
            userModel.Major = !string.IsNullOrEmpty(userRequestModel.Major) ? userRequestModel.Major.Trim() : string.Empty;
            userModel.Password = !string.IsNullOrEmpty(userRequestModel.Password) ? EncryptionUtility.EncryptString(userRequestModel.Password.Trim()) : string.Empty;
            userModel.DateOfBirth = userRequestModel.DateOfBirth ?? null;
            userModel.IsActive = userRequestModel.IsActive ?? null;

            UserDto updateUserResult = null;
            UserDto user = _mapper.Map<UserDto>(userModel);

            if (user != null)
            {
                updateUserResult = _userBaseRepository.UpdateUser(user);
            }

            return updateUserResult;
        }

        public UserDto GetActiveUserLogin(AuthRequestModel authRequestModel)
        {
            var userLogin = _userBaseRepository.GetActiveUserLogin(authRequestModel.Email, EncryptionUtility.EncryptString(authRequestModel.Password));

            return userLogin;
        }

        public UserDto GetUserById(long userId)
        {
            var userDetail = _userBaseRepository.GetUserById(userId);

            return userDetail;
        }

        public UserDto GetUserByEmail(string email)
        {
            var userDetail = _userBaseRepository.GetUserByEmail(email);

            return userDetail;
        }
    }
}
