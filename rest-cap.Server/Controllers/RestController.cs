using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rest_cap.Server.Entities;
using rest_cap.Server.Helpers;
using rest_cap.Server.Models;

namespace rest_cap.Server.Controllers
{
    [ApiController]
    [EnableCors("allowspecificorigin")]
    [Route("api")]
    public class RestController : ControllerBase
    {
        private readonly ILogger<RestController> _logger;
        private readonly StorageContext _storageContext;
        private readonly IMapper _mapper;

        public RestController(ILogger<RestController> logger, StorageContext context, IMapper mapper)
        {
            _logger = logger;
            _storageContext = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("rest/get_users")]
        [ServiceFilter(typeof(ApiAccessAuthorizeFilter))]
        public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string userName = null, [FromQuery] string email = null) {
            try
            {
                var usersQuery = _storageContext.Users.AsQueryable();

                if (!string.IsNullOrEmpty(userName))
                {
                    usersQuery = usersQuery.Where(u => u.UserName.Contains(userName));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    usersQuery = usersQuery.Where(u => u.Email.Contains(email));
                }

                var totalUsers = await usersQuery.CountAsync();

                var users = await usersQuery.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                var usersDTOs = _mapper.Map<List<UserDTO>>(users);

                var result = new
                {
                    TotalCount = totalUsers,
                    Page = page,
                    PageSize = pageSize,
                    Users = usersDTOs
                };
                return Ok(result);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while retrieving users");
                return StatusCode(500, "Internal Server Error!");
            }
        }
    }
}
