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
            bool emailSend = false;

            int counter = 0;

            var s3Template = new byte[0];

            var toList = new List<EmailAddress>();

            var emailTracks = new List<UserEmailTrackingDto>(); 

            if (mail != null)
            {
                try
                {
                    var msgSettings = new SendGridMessage();

                    var client = new SendGridClient(EncryptionHelper.DecryptString(_sendGridSettings.ApiKey));
                    var from = new EmailAddress(_sendGridSettings.SenderEmail, _sendGridSettings.SenderName);

                    foreach (var emailAddress in mail.To)
                    {
                        var email = new EmailAddress(emailAddress.Value);

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

                        var emailResponse = await client.SendEmailAsync(msg);

                        if(emailResponse.IsSuccessStatusCode)
                        {
                            if(mail.To.Count > 0)
                            {
                                foreach(var track in mail.To)
                                {
                                    var emailTrackDetail = new UserEmailTrackingDto()
                                    {
                                        UserId = track.Key,
                                        Email = track.Value,
                                        EmailSubject = mail.Subject,
                                        EmailTemplate = mail.S3TemplateFile,
                                        EmailStatus = "success",
                                        IsActive = true
                                    };

                                    emailTracks.Add(emailTrackDetail);

                                    emailSend = true;
                                }

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (mail.To.Count > 0)
                    {
                        foreach (var track in mail.To)
                        {
                            var emailTrackDetail = new UserEmailTrackingDto()
                            {
                                UserId = track.Key,
                                Email = track.Value,
                                EmailSubject = mail.Subject,
                                EmailTemplate = mail.S3TemplateFile,
                                EmailStatus = ex.Message,
                                IsActive = true
                            };

                            emailTracks.Add(emailTrackDetail);

                            emailSend = true;
                        }

                    }
                }
                finally
                {
                    // Save email tracking
                    _userBaseRepository.CreateUserEmailTracks(emailTracks);
                }
            }

            return emailSend;
        }

        public bool SendEmailVerificationCode(UserCodeRequestModel userCodeRequestModel)
        {
            bool sendMail = false;

            var getUser = _userBaseRepository.GetUserById(userCodeRequestModel.UserId);

            if(getUser != null)
            {
                var sendEmailModel = new SendEmailModel();

                var to = new Dictionary<long, string>();
                to.Add(getUser.Id, getUser.Email);

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

                sendEmailModel.To = to;
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
