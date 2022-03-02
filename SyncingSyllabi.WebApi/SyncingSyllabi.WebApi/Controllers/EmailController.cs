using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmailController : ControllerBase
    {
        IMapper _mapper;
        SendGridSettings _sendGridSettings;
        IEmailService _emailService;

        public EmailController
        (
            IMapper mapper,
            SendGridSettings sendGridSettings,
            IEmailService emailService
        )
        {
            _mapper = mapper;
            _sendGridSettings = sendGridSettings;
            _emailService = emailService;
        }

        [HttpPost]
        [Route("SendEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> SendEmail([FromForm] SendEmailModel sendEmailModel)
        {
            try
            {
                var emailAddress = new List<string>() { "ryan@creativeinterlace.com", "rai.masters010@gmail.com" };

                var emailXModel = new EmailVerificationEmailModel()
                {
                    FirstName = "Ryan",
                    VerificationCode = "123456"
                };

                var xModel = new List<string>()
                {
                    emailXModel.FirstName,
                    emailXModel.VerificationCode.ToString()
                };

                sendEmailModel.To = emailAddress;
                sendEmailModel.XModel = xModel;
                sendEmailModel.Subject = "Email Verification";
                sendEmailModel.S3TemplateFile = "EmailVerificationTemplate.html";

                var send = await _emailService.SendEmail(sendEmailModel);

                if(!send)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
