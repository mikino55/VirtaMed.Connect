using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VirtaMed.Connect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityTestController : ControllerBase
    {
        private readonly ILogger<IdentityTestController> logger;

        public IdentityTestController(ILogger<IdentityTestController> logger)
        {
            this.logger = logger;
        }
        [Route("checkAuthentication")]
        [Authorize]
        public IActionResult IsAuthenticated()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });

            var aaa = HttpContext.User;
            var name = User.Identity.Name;
            logger.LogInformation("claims: {claims}", claims);
            return Ok(claims);
        }
    }
}