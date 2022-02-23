using AutoMapper;
using SyncingSyllabi.Common.Tools.Helpers;
using SyncingSyllabi.Common.Tools.Utilities;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Repositories.Interfaces;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SyncingSyllabi.Services.Services
{

    public class UserService : IUserService
    {
        S3Settings _s3Settings;
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBaseRepository;
        private readonly IS3FileRepository _s3FileRepository;

        public UserService
        (
            S3Settings s3Settings,
            IMapper mapper,
            IUserBaseRepository userBaseRepository,
            IS3FileRepository s3FileRepository
        )
        {
            _s3Settings = s3Settings;
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
            _s3FileRepository = s3FileRepository;
        }

        public UserDto CreateUser(UserRequestModel userRequestModel)
        {
            UserDto createUserResult = null;

            var userModel = new UserModel();

            userModel.FirstName = !string.IsNullOrEmpty(userRequestModel.FirstName) ? userRequestModel.FirstName.Trim() : string.Empty;
            userModel.LastName = !string.IsNullOrEmpty(userRequestModel.LastName) ? userRequestModel.LastName.Trim() : string.Empty;
            userModel.Email = !string.IsNullOrEmpty(userRequestModel.Email) ? userRequestModel.Email.Trim() : string.Empty;
            userModel.School = !string.IsNullOrEmpty(userRequestModel.School) ? userRequestModel.School.Trim() : string.Empty;
            userModel.Major = !string.IsNullOrEmpty(userRequestModel.Major) ? userRequestModel.Major.Trim() : string.Empty;
            userModel.DateOfBirth = userRequestModel.DateOfBirth ?? null;
            userModel.Password = EncryptionUtility.EncryptString(userRequestModel.Password.Trim());
            userModel.IsActive = true;

            if(userRequestModel.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString();

                var fileBytes = FileHelper.FileMemoryStreamConverted(userRequestModel.ImageFile);

                if(fileBytes.Length > 0)
                {
                    _s3FileRepository.UploadFile(_s3Settings.UserFileDirectory, fileName, fileBytes);
                }

                userModel.ImageName = $"{fileName}{Path.GetExtension(userRequestModel.ImageFile.FileName)}";
                userModel.ImageUrl = $"{_s3Settings.UserFileDirectory}/{userModel.ImageName}";
            }

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

            if (userRequestModel.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString();

                var fileBytes = FileHelper.FileMemoryStreamConverted(userRequestModel.ImageFile);

                if (fileBytes.Length > 0)
                {
                    _s3FileRepository.UploadFile(_s3Settings.UserFileDirectory, fileName, fileBytes);
                }

                userModel.ImageName = $"{fileName}{Path.GetExtension(userRequestModel.ImageFile.FileName)}";
                userModel.ImageUrl = $"{_s3Settings.UserFileDirectory}/{userModel.ImageName}";
            }

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
