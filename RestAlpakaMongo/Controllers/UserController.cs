using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;

        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tickets>> GetUsers(string id)
        {
            var users = await _userService.GetByIdAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> CreateUsers(Users users)
        {
            await _userService.CreateAsync(users);
            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsers(string id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }
            await _userService.UpdateAsync(id, users);
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUsers(string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            await _userService.DeleteAsync(id);
            return NoContent();
        }

    }
}
