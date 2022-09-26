using AutoMapper;
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
        public IActionResult CreateAssignment([FromForm] AssignmentRequestModel assignmentRequestModel)
        {
            try
            {
                var result = _assignmentService.CreateAssignment(assignmentRequestModel);
                //var item = _mapper.Map<AssignmentModel>(result);

                //var response = new AssignmentResponseModel();

                //if (item != null)
                //{
                //    response.Data.Item = item;
                //}
                //else
                //{
                //    response.Data.Success = false;
                //}

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateAssignment")]
        public IActionResult UpdateAssignment([FromForm] AssignmentRequestModel assignmentRequestModel)
        {
            try
            {
                var result = _assignmentService.UpdateAssignment(assignmentRequestModel);
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

        [HttpGet]
        [Route("GetAssignmentDetails/{assignmentId}/{userId}")]
        public IActionResult GetAssignmentDetails(long assignmentId, long userId)
        {
            try
            {
                var result = _assignmentService.GetAssignment(assignmentId, userId);
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

        [HttpPost]
        [Route("GetAssignmentDetailsList")]
        public IActionResult GetAssignmentDetailsList([FromBody] AssignmentListRequestModel assignmentRequestModel)
        {
            try
            {
                var result = _assignmentService.GetAssignmentDetailsList(assignmentRequestModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DeleteAssignment/{assignmentId}/{userId}")]
        public IActionResult DeleteAssignment(long assignmentId, long userId)
        {
            try
            {
                var result = _assignmentService.DeleteAssignment(assignmentId, userId);

                var response = new DeleteResponseModel();

                response.Data.Success = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DeleteAssignmentAttachment/{assignmentId}")]
        public IActionResult DeleteAssignmentAttachment(long assignmentId)
        {
            try
            {
                var result = _assignmentService.DeleteAssignmentAttachment(assignmentId);

                var response = new DeleteResponseModel();

                response.Data.Success = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("DeleteAllAssignmentByUserId/{userId}")]
        public IActionResult DeleteAllAssignmentByUserId(long userId)
        {
            try
            {
                var result = _assignmentService.DeleteAllAssignmentByUserId(userId);

                var response = new DeleteResponseModel();

                response.Data.Success = result;

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
