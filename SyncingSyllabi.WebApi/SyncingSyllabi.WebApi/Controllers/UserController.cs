using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.Main.WebApi.Controllers
{
    public class UserController : ControllerBase
    {

        IMapper _mapper;
        IUserService _userService;

        public UserController
        (
            IMapper mapper,
            IUserService userService
        )
        {
            _mapper = mapper;
            _userService = userService;
        }


        [HttpGet]
        [Route("GetUserById/{id}")]
        public IActionResult GetUserById(long userId)
        {
            try
            {
                var result = _userService.GetUserById(userId);
                var response = _mapper.Map<IEnumerable<UserModel>>(result);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
