using AutoMapper;
using SyncingSyllabi.Common.Tools.Helpers;
using SyncingSyllabi.Data.Constants;
using SyncingSyllabi.Data.Dtos.Core;
using SyncingSyllabi.Data.Enums;
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
        private readonly S3Settings _s3Settings;
        private readonly IMapper _mapper;
        private readonly IUserBaseRepository _userBaseRepository;
        private readonly IS3FileRepository _s3FileRepository;
        private readonly IEmailService _emailService;

        public UserService
        (
            S3Settings s3Settings,
            IMapper mapper,
            IUserBaseRepository userBaseRepository,
            IS3FileRepository s3FileRepository,
            IEmailService emailService
        )
        {
            _s3Settings = s3Settings;
            _mapper = mapper;
            _userBaseRepository = userBaseRepository;
            _s3FileRepository = s3FileRepository;
            _emailService = emailService;
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
            userModel.Password = EncryptionHelper.EncryptString(userRequestModel.Password.Trim());
            userModel.IsActive = false;

            if(userRequestModel.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString();

                var fileBytes = FileHelper.FileMemoryStreamConverter(userRequestModel.ImageFile);

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

                if(createUserResult != null)
                {
                    var sendEmailModel = new SendEmailModel();

                    var emailAddress = new List<string>() { createUserResult.Email };

                    var emailXModel = new EmailVerificationEmailModel()
                    {
                        FirstName = !string.IsNullOrWhiteSpace(createUserResult.FirstName) ?  createUserResult.FirstName :  "User",
                        VerificationCode = KeyCodeHelper.GenerateRandomIntegerCode()
                    };

                    var xModel = new List<string>()
                    {
                        emailXModel.FirstName,
                        emailXModel.VerificationCode
                    };

                    sendEmailModel.To = emailAddress;
                    sendEmailModel.XModel = xModel;
                    sendEmailModel.Subject = "Email Verification";
                    sendEmailModel.S3TemplateFile = EmailTemplateConstants.EmailVerificationTemplate;

                    var send = _emailService.SendEmail(sendEmailModel).GetAwaiter().GetResult();

                    if(send)
                    {
                        var userCode = new UserCodeDto()
                        { 
                            UserId = createUserResult.Id,
                            VerificationCode = emailXModel.VerificationCode,
                            CodeType = CodeTypeEnum.EmailVerificationCode,
                            CodeTypeName = CodeTypeEnum.EmailVerificationCode.ToString(),
                            IsActive = true
                        };

                        var createCode = _userBaseRepository.CreateUserCode(userCode);

                        if(createCode == null)
                        {
                            createUserResult = null;
                        }
                    }
                }
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
            userModel.Password = !string.IsNullOrEmpty(userRequestModel.Password) ? EncryptionHelper.EncryptString(userRequestModel.Password.Trim()) : string.Empty;
            userModel.DateOfBirth = userRequestModel.DateOfBirth ?? null;
            userModel.IsActive = userRequestModel.IsActive ?? null;

            if (userRequestModel.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString();

                var fileBytes = FileHelper.FileMemoryStreamConverter(userRequestModel.ImageFile);

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
            var userLogin = _userBaseRepository.GetActiveUserLogin(authRequestModel.Email, EncryptionHelper.EncryptString(authRequestModel.Password));

            return userLogin;
        }

        public UserDto UserLogin(AuthRequestModel authRequestModel)
        {
            var userLogin = _userBaseRepository.UserLogin(authRequestModel.Email, EncryptionHelper.EncryptString(authRequestModel.Password));

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

        public bool VerifyUserCode(UserCodeRequestModel userCodeRequestModel)
        {
            bool verify = false;

            var getUserCode = _userBaseRepository.GetUserCode(userCodeRequestModel.UserId, userCodeRequestModel.CodeType);

            if(getUserCode != null && getUserCode.CodeExpiration.Value > DateTime.UtcNow)
            {
                if(getUserCode.VerificationCode == userCodeRequestModel.VerificationCode)
                {
                    var userModel = new UserModel();
                    userModel.Id = userCodeRequestModel.UserId;
                    userModel.IsActive = true;

                    UserDto user = _mapper.Map<UserDto>(userModel);

                    var activateUser = _userBaseRepository.UpdateUser(user);

                    if(activateUser != null)
                    {
                        var userCode = new UserCodeDto()
                        {
                            UserId = userCodeRequestModel.UserId,
                            CodeType = userCodeRequestModel.CodeType,
                            VerificationCode = string.Empty,
                            IsActive = false
                        };

                        var updateUserCode = _userBaseRepository.UpdateUserCode(userCode);

                        if(updateUserCode != null)
                        {
                            verify = true;
                        }
                    }
                }
            }

            return verify;
        }
    }
}
