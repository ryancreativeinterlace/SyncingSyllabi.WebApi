using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncingSyllabi.Data.Models.Request;
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
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public NotificationController
        (
            IMapper mapper,
            INotificationService notificationService
        )
        {
            _mapper = mapper;
            _notificationService = notificationService;
        }

        [HttpPost]
        [Route("NotificationToken")]
        public IActionResult NotificationToken([FromBody] NotificationTokenRequestModel notificationTokenRequestModel)
        {
            try
            {
                var result = _notificationService.UpdateUserNotification(notificationTokenRequestModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SendNotification")]
        public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequestModel sendNotificationRequestModel)
        {
            try
            {
                var result = await _notificationService.SendNotification(sendNotificationRequestModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UserNotificationList")]
        public IActionResult UserNotificationList([FromBody] UserNotificationListRequestModel userNotificationListRequestModel)
        {
            try
            {
                var result = _notificationService.GetUserNotificationList(userNotificationListRequestModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("ReadNotification")]
        public IActionResult ReadNotification([FromBody] ReadNotificationRequestModel readNotificationRequestModel)
        {
            try
            {
                var result = _notificationService.ReadNotification(readNotificationRequestModel.NotificationId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RemoveNotification")]
        public IActionResult RemoveNotification([FromBody] ReadNotificationRequestModel readNotificationRequestModel)
        {
            try
            {
                var result = _notificationService.RemoveNotification(readNotificationRequestModel.NotificationId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
