
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncingSyllabi.Data.Settings;
using SyncingSyllabi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        DatabaseSettings _databaseSettings;

        public HealthCheckController
        (
            DatabaseSettings databaseSettings
        )
        {
            _databaseSettings = databaseSettings;
        }

        [HttpGet]
        [Route("monitor")]
        public IActionResult Monitor()
        {
            return Ok($"{_databaseSettings.Environment} Environment - Syncing Syllabi Web App: {DateTime.UtcNow}");
        }
    }
}
