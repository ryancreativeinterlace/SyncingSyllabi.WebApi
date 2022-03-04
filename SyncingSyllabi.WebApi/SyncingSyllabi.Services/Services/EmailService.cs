using SendGrid;
using SendGrid.Helpers.Mail;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncingSyllabi.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly S3Settings _s3Settings;
        private readonly SendGridSettings _sendGridSettings;
        private readonly IS3FileRepository _s3FileRepository;
        private readonly IUserBaseRepository _userBaseRepository;

        public EmailService
        (
            S3Settings s3Settings,
            SendGridSettings sendGridSettings,
            IS3FileRepository s3FileRepository,
            IUserBaseRepository userBaseRepository
        )
        {
            _s3Settings = s3Settings;
            _sendGridSettings = sendGridSettings;
            _s3FileRepository = s3FileRepository;
            _userBaseRepository = userBaseRepository;

        }

        public async Task<bool> SendEmail(SendEmailModel mail)
        {
            int counter = 0;

            var s3Template = new byte[0];

            var toList = new List<EmailAddress>();

            if (mail != null)
            {
                try
                {
                    var msgSettings = new SendGridMessage();

                    var client = new SendGridClient(EncryptionHelper.DecryptString(_sendGridSettings.ApiKey));
                    var from = new EmailAddress(_sendGridSettings.SenderEmail, _sendGridSettings.SenderName);

                    foreach (var emailAddress in mail.To)
                    {
                        var email = new EmailAddress(emailAddress);

                        toList.Add(email);
                    }

                    if (!string.IsNullOrWhiteSpace(mail.S3TemplateFile))
                    {
                        s3Template = await _s3FileRepository.DownloadFile(_s3Settings.EmailTemplateDirectory, mail.S3TemplateFile);
                    }

                    if (s3Template.Length > 0)
                    {
                        Stream stream = new MemoryStream(s3Template);

                        StreamReader streamReader = new StreamReader(stream);

                        var reaSource = streamReader.ReadToEnd();

                        var htmlContent = string.Empty;

                        var xModel = string.Empty;

                        foreach (var model in mail.XModel)
                        {

                            if (counter == 0)
                            {
                                htmlContent = string.Format(reaSource, model, string.Empty);

                                xModel = model;
                            }
                            else
                            {
                                htmlContent = string.Format(reaSource, xModel, model);
                            }

                            counter++;
                        }

                        var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, toList, mail.Subject, string.Empty, htmlContent);

                        if (mail.Attachment != null && mail.Attachment.Count() > 0)
                        {
                            foreach (var item in mail.Attachment)
                            {
                                if (item.FileData != null)
                                {
                                    var attachFile = new Attachment
                                    {
                                        Content = item.FileData,
                                        Filename = item.FileName,
                                        Type = item.Type,
                                        Disposition = "attachment"
                                    };

                                    msg.AddAttachment(attachFile);
                                }
                            }
                        }

                        await client.SendEmailAsync(msg);

                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

            return false;
        }

        public bool SendEmailVerificationCode(UserCodeRequestModel userCodeRequestModel)
        {
            bool sendMail = false;

            var getUser = _userBaseRepository.GetUserById(userCodeRequestModel.UserId);

            if(getUser != null)
            {
                var sendEmailModel = new SendEmailModel();

                var emailAddress = new List<string>() { getUser.Email };

                var emailXModel = new EmailVerificationEmailModel()
                {
                    FirstName = !string.IsNullOrWhiteSpace(getUser.FirstName) ? getUser.FirstName : "User",
                    VerificationCode = KeyCodeHelper.GenerateRandomIntegerCode()
                };

                var xModel = new List<string>()
                {
                    emailXModel.FirstName,
                    emailXModel.VerificationCode
                };

                sendEmailModel.To = emailAddress;
                sendEmailModel.XModel = xModel;

                switch(userCodeRequestModel.CodeType)
                {
                    case CodeTypeEnum.EmailVerificationCode:
                        sendEmailModel.Subject = "Email Verification";
                        sendEmailModel.S3TemplateFile = EmailTemplateConstants.EmailVerificationTemplate;
                        break;

                    case CodeTypeEnum.ChangePassword:
                        sendEmailModel.Subject = "Change Password";
                        sendEmailModel.S3TemplateFile = EmailTemplateConstants.ChangePasswordTemplate;
                        break;

                }

                var send = this.SendEmail(sendEmailModel).GetAwaiter().GetResult();

                if (send)
                {
                    var getUserCode = _userBaseRepository.GetUserCode(userCodeRequestModel.UserId, userCodeRequestModel.CodeType);

                    if (getUserCode != null)
                    {
                        getUserCode.VerificationCode = emailXModel.VerificationCode;
                        getUserCode.CodeType = userCodeRequestModel.CodeType;
                        getUserCode.CodeTypeName = userCodeRequestModel.CodeType.ToString();
                        getUserCode.IsActive = true;
                        getUserCode.CodeExpiration = null;

                        var updateUserCode = _userBaseRepository.UpdateUserCode(getUserCode);

                        if(updateUserCode != null)
                        {
                            sendMail = true;
                        }
                    }
                }
            }

            return sendMail;
        }
    }
}
