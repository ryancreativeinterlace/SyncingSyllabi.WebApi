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
