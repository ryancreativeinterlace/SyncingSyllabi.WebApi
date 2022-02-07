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
    [Route("api/[controller]")]
    [ApiController]
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
        [Route("getuserbyid")]
        public IActionResult GetUserById()
        {
            try
            {
                long userId = 1;

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
