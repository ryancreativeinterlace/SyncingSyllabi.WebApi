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
            userModel.Password = !string.IsNullOrEmpty(userRequestModel.Password) ? EncryptionHelper.EncryptString(userRequestModel.Password.Trim()) : string.Empty;
            userModel.IsEmailConfirm = false;
            userModel.IsResetPassword = false;
            userModel.IsGoogle = userRequestModel.IsGoogle ?? null;

            if(userRequestModel.IsGoogle.HasValue && userRequestModel.IsGoogle.Value)
            {
                userModel.IsActive = true;
            }
            else
            {
                userModel.IsActive = false;
            }

            if(userRequestModel.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString();

                var fileBytes = FileHelper.FileMemoryStreamConverter(userRequestModel.ImageFile);

                if(fileBytes.Length > 0)
                {
                    _s3FileRepository.UploadFile(_s3Settings.UserFileDirectory, fileName, fileBytes);
                }

                userModel.ImageName = fileName;
                userModel.ImageUrl = $"{_s3Settings.UserFileDirectory}/{userModel.ImageName}";
            }

            UserDto user = _mapper.Map<UserDto>(userModel);
            
            if(user != null)
            {
                createUserResult = _userBaseRepository.CreateUser(user);
                
                if(!string.IsNullOrEmpty(createUserResult.ImageUrl))
                {
                    // Get Presigned URL
                    createUserResult.ImageUrl = _s3FileRepository.GetPreSignedUrl(_s3Settings.UserFileDirectory, createUserResult.ImageName, string.Empty, string.Empty, DateTime.Now.AddDays(2));
                }

                // Send email verification
                if(createUserResult != null && !createUserResult.IsGoogle.Value)
                {
                    var sendEmailModel = new SendEmailModel();

                    var emailTo = new Dictionary<long, string>();

                    emailTo.Add(createUserResult.Id, createUserResult.Email);

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

                    sendEmailModel.To = emailTo;
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
            userModel.IsEmailConfirm = userRequestModel.IsEmailConfirm ?? null;
            userModel.IsResetPassword = userRequestModel.IsResetPassword ?? null;
            userModel.IsGoogle = userRequestModel.IsGoogle ?? null;

            if (userRequestModel.ImageFile != null)
            {
                var fileName = Guid.NewGuid().ToString();

                var fileBytes = FileHelper.FileMemoryStreamConverter(userRequestModel.ImageFile);

                if (fileBytes.Length > 0)
                {
                    _s3FileRepository.UploadFile(_s3Settings.UserFileDirectory, fileName, fileBytes);
                }

                userModel.ImageName = fileName;
                userModel.ImageUrl = $"{_s3Settings.UserFileDirectory}/{userModel.ImageName}";
            }

            UserDto updateUserResult = null;
            UserDto user = _mapper.Map<UserDto>(userModel);

            if (user != null)
            {
                updateUserResult = _userBaseRepository.UpdateUser(user);

                if (!string.IsNullOrEmpty(updateUserResult.ImageUrl))
                {
                    // Get Presigned URL
                    updateUserResult.ImageUrl = _s3FileRepository.GetPreSignedUrl(_s3Settings.UserFileDirectory, updateUserResult.ImageName, string.Empty, string.Empty, DateTime.Now.AddDays(2));
                }
            }

            return updateUserResult;
        }

        public UserDto GetActiveUserLogin(AuthRequestModel authRequestModel)
        {
            var userLogin = _userBaseRepository.GetActiveUserLogin(authRequestModel.Email, EncryptionHelper.EncryptString(authRequestModel.Password), authRequestModel.IsGoogle);

            if (!string.IsNullOrEmpty(userLogin.ImageUrl))
            {
                // Get Presigned URL
                userLogin.ImageUrl = _s3FileRepository.GetPreSignedUrl(_s3Settings.UserFileDirectory, userLogin.ImageName, string.Empty, string.Empty, DateTime.Now.AddDays(2));
            }

            return userLogin;
        }

        public UserDto UserLogin(AuthRequestModel authRequestModel)
        {
            var userLogin = _userBaseRepository.UserLogin(authRequestModel.Email, EncryptionHelper.EncryptString(authRequestModel.Password), authRequestModel.IsGoogle);

            if (!string.IsNullOrEmpty(userLogin.ImageUrl))
            {
                // Get Presigned URL
                userLogin.ImageUrl = _s3FileRepository.GetPreSignedUrl(_s3Settings.UserFileDirectory, userLogin.ImageName, string.Empty, string.Empty, DateTime.Now.AddDays(2));
            }

            return userLogin;
        }

        public UserDto GetUserById(long userId)
        {
            var userDetail = _userBaseRepository.GetUserById(userId);

            if (!string.IsNullOrEmpty(userDetail.ImageUrl))
            {
                // Get Presigned URL
                userDetail.ImageUrl = _s3FileRepository.GetPreSignedUrl(_s3Settings.UserFileDirectory, userDetail.ImageName, string.Empty, string.Empty, DateTime.Now.AddDays(2));
            }

            return userDetail;
        }

        public UserDto GetUserByEmail(string email)
        {
            var userDetail = _userBaseRepository.GetUserByEmail(email);

            if (!string.IsNullOrEmpty(userDetail.ImageUrl))
            {
                // Get Presigned URL
                userDetail.ImageUrl = _s3FileRepository.GetPreSignedUrl(_s3Settings.UserFileDirectory, userDetail.ImageName, string.Empty, string.Empty, DateTime.Now.AddDays(2));
            }

            return userDetail;
        }

        public bool VerifyUserCode(UserCodeRequestModel userCodeRequestModel)
        {
            bool verify = false;

            var getUserCode = _userBaseRepository.GetUserCode(userCodeRequestModel.UserId, userCodeRequestModel.CodeType);

            if(getUserCode != null && getUserCode.CodeExpiration.Value > DateTime.Now)
            {
                if(getUserCode.VerificationCode == userCodeRequestModel.VerificationCode)
                {
                    if(userCodeRequestModel.CodeType == CodeTypeEnum.EmailVerificationCode)
                    {
                        var userModel = new UserModel();
                        userModel.Id = userCodeRequestModel.UserId;
                        userModel.IsActive = true;
                        userModel.IsEmailConfirm = true;

                        UserDto user = _mapper.Map<UserDto>(userModel);

                        var activateUser = _userBaseRepository.UpdateUser(user);

                        if (activateUser != null)
                        {
                            getUserCode.IsActive = false;

                            var updateUserCode = _userBaseRepository.UpdateUserCode(getUserCode);

                            if (updateUserCode != null)
                            {
                                verify = true;
                            }
                        }
                    }
                    else
                    {
                        verify = true;
                    }
                }
            }

            return verify;
        }

        public bool ResetPassword(UserPasswordRequestModel userPasswordRequestModel)
        {
            bool resetPassword = false;

            var getUser = _userBaseRepository.GetUserById(userPasswordRequestModel.UserId);

            if(getUser != null && getUser.Password == EncryptionHelper.EncryptString(userPasswordRequestModel.CurrentPassword))
            {
                UserDto updateUser = new UserDto();

                updateUser.Id = getUser.Id;
                updateUser.Password = EncryptionHelper.EncryptString(userPasswordRequestModel.UpdatedPassword);
                updateUser.IsResetPassword = false;

                var updateUserPassword = _userBaseRepository.UpdateUser(updateUser);

                if(updateUserPassword != null)
                {
                    var getUserCode = _userBaseRepository.GetUserCode(userPasswordRequestModel.UserId, CodeTypeEnum.ChangePassword);
                    
                    if(getUserCode != null)
                    {
                        // Update User Code
                        getUserCode.IsActive = false;

                        var updateUserCode = _userBaseRepository.UpdateUserCode(getUserCode);

                        if (updateUserCode != null)
                        {
                            resetPassword = true;
                        }
                    }
                }
            }

            return resetPassword;
        }

        public string DecryptPassword(UserPasswordDecryptRequestModel userPasswordDecryptRequestModel)
        {
            string decryptedPassword = string.Empty;

            if(!string.IsNullOrEmpty(userPasswordDecryptRequestModel.DecryptPassword))
            {
                decryptedPassword = EncryptionHelper.DecryptString(userPasswordDecryptRequestModel.DecryptPassword);
            }

            return decryptedPassword;
        }
    }
}
