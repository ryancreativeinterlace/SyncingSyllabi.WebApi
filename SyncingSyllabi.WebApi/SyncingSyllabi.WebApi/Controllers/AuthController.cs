﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController
        (
            IMapper mapper,
            IAuthService authService
        )
        {
            _mapper = mapper;
            _authService = authService;
        }

        ///<Summary>
        /// Generate user authentication token.
        ///</Summary>
        ///<returns>The user generated authentication token.</returns>
        [HttpPost]
        [Route("GenerateAuth")]
        [AllowAnonymous]
        public IActionResult GenerateAuth([FromBody] AuthRequestModel authRequestModel)
        {
            try
            {
                var result = _authService.GetAuthToken(authRequestModel);
                var item = _mapper.Map<AuthModel>(result);

                var response = new AuthResponseModel();
                
                if(item != null)
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
    }
}
