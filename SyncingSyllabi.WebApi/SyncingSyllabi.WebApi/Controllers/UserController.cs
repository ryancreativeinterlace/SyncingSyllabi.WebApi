using AutoMapper;
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

namespace SyncingSyllabi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

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
        [AllowAnonymous]
        public IActionResult CreateUser([FromForm] UserRequestModel userModel)
        {
            try
            {
                var result = _userService.CreateUser(userModel);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();

                if (item != null)
                {
                    response.Data.Item = item;
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

        [HttpPost]
        [Route("UpdateUser")]
        public IActionResult UpdateUser([FromForm] UserRequestModel userModel)
        {
            try
            {
                var result = _userService.UpdateUser(userModel);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();

                if (item != null)
                {
                    response.Data.Item = item;
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

        [HttpPost]
        [Route("LoginUser")]
        [AllowAnonymous]
        public IActionResult LoginUser([FromBody] AuthRequestModel authRequestModel)
        {
            try
            {
                var result = _userService.UserLogin(authRequestModel);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();

                if (item != null)
                {
                    response.Data.Item = item;
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

        [HttpGet]
        [Route("GetUserById/{userId}")]
        public IActionResult GetUserById(long userId)
        {
            try
            {
                var result = _userService.GetUserById(userId);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();

                if (item != null)
                {
                    response.Data.Item = item;
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

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        [AllowAnonymous]
        public IActionResult GetUserByEmail(string email)
        {
            try
            {
                var result = _userService.GetUserByEmail(email);
                var item = _mapper.Map<UserModel>(result);

                var response = new UserResponseModel();

                if (item != null)
                {
                    response.Data.Item = item;
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

        [HttpPost]
        [Route("VerifyUserCode")]
        [AllowAnonymous]
        public IActionResult VerifyUserCode([FromBody] UserCodeRequestModel userCodeRequestModel)
        {
            try
            {
                var result = _userService.VerifyUserCode(userCodeRequestModel);
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

        [HttpPost]
        [Route("ResetPassword")]
        [AllowAnonymous]
        public IActionResult ResetPassword([FromBody] UserPasswordRequestModel userPasswordRequestModel)
        {
            try
            {
                var result = _userService.ResetPassword(userPasswordRequestModel);
                var response = new UserPasswordResponseModel();

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

        [HttpPost]
        [Route("DecryptPassword")]
        [AllowAnonymous]
        public IActionResult DecryptPassword([FromBody] UserPasswordDecryptRequestModel userPasswordDecryptRequestModel)
        {
            try
            {
                var result = _userService.DecryptPassword(userPasswordDecryptRequestModel);
                var response = new UserDecryptPasswordResponseModel();

                if (!string.IsNullOrEmpty(result))
                {
                    response.Data.DecryptedPassword = result;
                }
                else
                {
                    response.Data.DecryptedPassword = string.Empty;
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DeleteUserAccount/{userId}/{isActive}")]
        public IActionResult DeleteUserAccount(long userId, bool isActive)
        {
            try
            {
                var result = _userService.DeleteUserAccount(userId, isActive);
                var response = new UserPasswordResponseModel();

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

        [HttpGet]
        [Route("GetAppleUserToken")]
        [AllowAnonymous]
        public IActionResult GetAppleUserToken()
        {
            try
            {
                var result = _userService.GetAppleUserToken();
                var response = new AppleAuthResponseModel();

                if (!string.IsNullOrEmpty(result))
                {
                    response.Data.AppleToken = result;
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

        [HttpGet]
        [Route("HardDeleteUser/{email}")]
        [AllowAnonymous]
        public IActionResult HardDeleteUser(string email)
        {
            try
            {
                var result = _userService.HardDeleteUserAccount(email);
                var response = new UserPasswordResponseModel();

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
