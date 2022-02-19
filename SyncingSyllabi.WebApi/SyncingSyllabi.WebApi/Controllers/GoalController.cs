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

namespace SyncingSyllabi.Main.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GoalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IGoalService _goalService;
        public GoalController
        (
            IMapper mapper,
            IGoalService goalService
        )
        {
            _mapper = mapper;
            _goalService = goalService;
        }

        [HttpPost]
        [Route("CreateGoal")]
        public IActionResult CreateGoal([FromBody] GoalRequestModel goalRequestModel)
        {
            try
            {
                var result = _goalService.CreateGoal(goalRequestModel);
                var item = _mapper.Map<GoalModel>(result);

                var response = new GoalResponseModel();
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
