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
    public class AssignmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentService _assignmentService;
        public AssignmentController
        (
            IMapper mapper,
            IAssignmentService assignmentService
        )
        {
            _mapper = mapper;
            _assignmentService = assignmentService;
        }

        [HttpPost]
        [Route("CreateAssignment")]
        public IActionResult CreateAssignment([FromBody] AssignmentRequestModel assignmentRequestModel)
        {
            try
            {
                var result = _assignmentService.CreateAssignment(assignmentRequestModel);
                var item = _mapper.Map<AssignmentModel>(result);

                var response = new AssignmentResponseModel();

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
    }
}