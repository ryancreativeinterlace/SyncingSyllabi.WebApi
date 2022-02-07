using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.Main.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsersController : ControllerBase
    {
        IMapper _mapper;
        IUserService _userService;

        public UsersController
        (
            IMapper mapper,
            IUserService userService
        )
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        [Route("GetUserById/{userId}")]
        public IActionResult GetUserById(long userId)
        {
            try
            {
                var result = _userService.GetUserById(userId);
                var response = _mapper.Map<UserModel>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
