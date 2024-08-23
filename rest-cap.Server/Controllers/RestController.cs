using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_cap.Server.Entities;

namespace rest_cap.Server.Controllers
{
    [ApiController]
    [EnableCors("allowspecificorigin")]
    [Route("api")]
    public class RestController : ControllerBase
    {
        private readonly ILogger<RestController> _logger;
        private readonly StorageContext _storageContext;

        public RestController(ILogger<RestController> logger, StorageContext context)
        {
            _logger = logger;
            _storageContext = context;
        }

        [HttpGet]
        [Route("rest/get_users")]
        public async Task<IActionResult> GetUsers() {
            try
            {
                var users = await _storageContext.Users.ToListAsync();

                if (users == null || users.Count == 0) { return NotFound("No users found!"); }

                return Ok(users);
            }
            catch(Exception ex) {
                _logger.LogError(ex, "An error occurred while retrieving users");

                return StatusCode(500, "Internal Server Error!");
            }
        }
    }
}
