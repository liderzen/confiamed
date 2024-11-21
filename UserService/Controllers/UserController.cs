using Microsoft.AspNetCore.Mvc;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {

            var users = new List<object>
            {
                new { Username = "Galo", PendingItems = 12},
                new { Username = "Santiago", PendingItems = 5}
            };

            return Ok(users);
        }
        
    }
}
