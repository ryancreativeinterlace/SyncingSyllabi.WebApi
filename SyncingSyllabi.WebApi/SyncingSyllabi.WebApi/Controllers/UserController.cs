﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncingSyllabi.Data.Models.Core;
using SyncingSyllabi.Data.Models.Request;
using SyncingSyllabi.Data.Models.Response;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.Main.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpPost]
        [Route("CreateUser")]
        public IActionResult CreateUser([FromBody] UserModel userModel)
        {
            try
            {
                var result = _userService.CreateUser(userModel);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();
                response.Data.Item = item;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser([FromBody] AuthRequestModel authRequestModel)
        {
            try
            {
                var result = _userService.GetActiveUserLogin(authRequestModel);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();
                response.Data.Item = item;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserById/{userId}")]
        public IActionResult GetUserById(long userId)
        {
            try
            {
                var result = _userService.GetUserById(userId);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();
                response.Data.Item = item;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var result = _userService.GetUserByEmail(email);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();
                response.Data.Item = item;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}