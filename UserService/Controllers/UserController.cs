using Microsoft.AspNetCore.Mvc;
using UserService.BLL.Interfaces;
using UserService.DAL.Entities;
using UserService.DAL.Repositories;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {


        private readonly UserRepository _repository;

        public UserController()
        {
            // Cadena de conexión por la de tu entorno
            var connectionString = "Server=localdb;Database=GQU;Trusted_Connection=True;";
            _repository = new UserRepository(connectionString);
        }

        private readonly IUserService _userService;

        /*public UserController(IUserService userService)
        {
            _userService = userService;
        }*/

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID del usuario.");
            }

            _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }

    }
}
