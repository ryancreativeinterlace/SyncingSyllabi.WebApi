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
    public class SyllabusController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISyllabusService _syllabusService;
        public SyllabusController
        (
            IMapper mapper,
            ISyllabusService syllabusService
        )
        {
            _mapper = mapper;
            _syllabusService = syllabusService;
        }

        [HttpPost]
        [Route("CreateSyllabus")]
        public IActionResult CreateSyllabus([FromBody] SyllabusRequestModel syllabusRequestModel)
        {
            try
            {
                var result = _syllabusService.CreateSyllabus(syllabusRequestModel);
                var item = _mapper.Map<SyllabusModel>(result);

                var response = new SyllabusResponseModel();

                if (item != null)
                {
                    var itemResult = new SyllabusDataOutputModel()
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        ClassCode = item.ClassCode,
                        ClassName = item.ClassName,
                        TeacherName = item.TeacherName,
                        ColorInHex = item.ColorInHex,
                        ClassSchedule = item.ClassSchedule.Split("|").ToList(),
                        CreatedBy = item.CreatedBy,
                        DateCreated = item.DateCreated,
                        UpdatedBy = item.UpdatedBy,
                        DateUpdated = item.DateUpdated,
                        IsActive = item.IsActive
                    };

                    response.Data.Item = itemResult;
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
        [Route("UpdateSyllabus")]
        public IActionResult UpdateSyllabus([FromBody] SyllabusRequestModel syllabusRequestModel)
        {
            try
            {
                var result = _syllabusService.UpdateSyllabus(syllabusRequestModel);
                var item = _mapper.Map<SyllabusModel>(result);

                var response = new SyllabusResponseModel();

                if (item != null)
                {
                    var itemResult = new SyllabusDataOutputModel()
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        ClassCode = item.ClassCode,
                        ClassName = item.ClassName,
                        TeacherName = item.TeacherName,
                        ColorInHex = item.ColorInHex,
                        ClassSchedule = item.ClassSchedule.Split("|").ToList(),
                        CreatedBy = item.CreatedBy,
                        DateCreated = item.DateCreated,
                        UpdatedBy = item.UpdatedBy,
                        DateUpdated = item.DateUpdated,
                        IsActive = item.IsActive
                    };

                    response.Data.Item = itemResult;
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
        [Route("GetSyllabusDetails/{syllabusId}/{userId}")]
        public IActionResult GetSyllabusDetails(long syllabusId, long userId)
        {
            try
            {
                var result = _syllabusService.GetSyllabus(syllabusId, userId);
                var item = _mapper.Map<SyllabusModel>(result);

                var response = new SyllabusResponseModel();

                if (item != null)
                {
                    var itemResult = new SyllabusDataOutputModel()
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        ClassCode = item.ClassCode,
                        ClassName = item.ClassName,
                        TeacherName = item.TeacherName,
                        ColorInHex = item.ColorInHex,
                        ClassSchedule = item.ClassSchedule.Split("|").ToList(),
                        CreatedBy = item.CreatedBy,
                        DateCreated = item.DateCreated,
                        UpdatedBy = item.UpdatedBy,
                        DateUpdated = item.DateUpdated,
                        IsActive = item.IsActive
                    };

                    response.Data.Item = itemResult;
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
        [Route("GetSyllabusDetailsList")]
        public IActionResult GetSyllabusDetailsList([FromBody] SyllabusRequestModel syllabusRequestModel)
        {
            try
            {
                var error = new List<string>();

                var result = _syllabusService.GetSyllabusDetailsList(syllabusRequestModel);

                var response = new SyllabusListResponseModel();

                if (result.Items.Count() > 0)
                {
                    response.Data = result;
                }
                else
                {

                    error.Add("No result or UserId dont Exist,");

                    response.Errors = error;
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
        [Route("DeleteSyllabus/{syllabusId}/{userId}")]
        public IActionResult DeleteSyllabus(long syllabusId, long userId)
        {
            try
            {
                var result = _syllabusService.DeleteSyllabus(syllabusId, userId);

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
