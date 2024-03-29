﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
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
        private readonly IEmailService _emailService;

        public EmailController
        (
            IEmailService emailService
        )
        {
            _emailService = emailService;
        }

        //[HttpPost]
        //[Route("SendEmailTest")]
        //[AllowAnonymous]
        //public async Task<IActionResult> SendEmail([FromBody] SendEmailModel sendEmailModel)
        //{
        //    try
        //    {
        //        var emailAddress = new List<string>() { "ryan@creativeinterlace.com", "rai.masters010@gmail.com" };

        //        var emailXModel = new EmailVerificationEmailModel()
        //        {
        //            FirstName = "Ryan",
        //            VerificationCode = "123456"
        //        };

        //        var xModel = new List<string>()
        //        {
        //            emailXModel.FirstName,
        //            emailXModel.VerificationCode.ToString()
        //        };

        //        sendEmailModel.To = emailAddress;
        //        sendEmailModel.XModel = xModel;
        //        sendEmailModel.Subject = "Email Verification";
        //        sendEmailModel.S3TemplateFile = "EmailVerificationTemplate.html";

        //        var send = await _emailService.SendEmail(sendEmailModel);

        //        if(!send)
        //        {
        //            return BadRequest();
        //        }

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost]
        [Route("SendEmailVerificationCode")]
        [AllowAnonymous]
        public IActionResult SendEmailVerificationCode([FromBody] UserCodeRequestModel userCodeRequestModel)
        {
            try
            {
               
                var result = _emailService.SendEmailVerificationCode(userCodeRequestModel);
                var response = new UserCodeResponseModel();

                if (result)
                {
                    response.Data.Success = true;
                }
                else
                {
                    response.Data.Success = false;
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
