using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UsresManager _usersManager;

        public UserController(AlpakaDbContext context)
        {
            _usersManager = new UsresManager(context);

        }


        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> Get()
        {
            return Ok(await _usersManager.GetAllAsync());
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> Get(int id)
        {
            var U = await _usersManager.GetByIdAsync(id);
            if (U == null)
            {
                return NotFound();
            }
            return Ok(U);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<Users>> Post(Users u)
        {
            await _usersManager.InsertAsync(u);
            return CreatedAtAction(nameof(Get), new { id = u.User_id }, u); // Assuming 'Id' is the identifier property
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Users u)
        {
            if (id != u.User_id) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _usersManager.UpdateAsync(u);
            return NoContent();
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _usersManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
