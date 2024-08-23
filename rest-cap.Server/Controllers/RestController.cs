using Microsoft.AspNetCore.Mvc;

namespace rest_cap.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<RestController> _logger;

        public RestController(ILogger<RestController> logger)
        {
            _logger = logger;
        }

    }
}
