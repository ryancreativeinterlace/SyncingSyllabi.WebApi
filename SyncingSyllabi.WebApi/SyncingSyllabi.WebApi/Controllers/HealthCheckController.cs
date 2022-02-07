using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SyncingSyllabi.Main.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [Route("monitor")]
        public IActionResult Monitor()
        {
            return Ok($"Syncing Syllabi Web App: {DateTime.Now}");
        }
    }
}
