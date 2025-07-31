using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Services;

namespace WebMVC.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersApiController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersApiController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/UsersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/UsersApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: api/UsersApi
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            if (await _userService.EmailExistsAsync(user.Email))
            {
                return BadRequest("Email already exists");
            }

            var createdUser = await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

        // PUT: api/UsersApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userService.UpdateUserAsync(user);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/UsersApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        // POST: api/UsersApi/authenticate
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> Authenticate([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isAuthenticated = await _userService.AuthenticateUserAsync(model.Email, model.Password);
            if (isAuthenticated)
            {
                var user = await _userService.GetUserByEmailAsync(model.Email);
                if (user != null)
                {
                    await _userService.UpdateLastLoginAsync(user.Id);
                    return Ok(new {
                        message = "Authentication successful",
                        user = new {
                            id = user.Id,
                            email = user.Email,
                            firstName = user.FirstName,
                            lastName = user.LastName
                        }
                    });
                }
            }

            return Unauthorized(new { message = "Invalid credentials" });
        }

        // GET: api/UsersApi/active
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUsers()
        {
            var users = await _userService.GetActiveUsersAsync();
            return Ok(users);
        }
    }
}