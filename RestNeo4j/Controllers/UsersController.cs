using System;
using Microsoft.AspNetCore.Mvc;
using Neoflix.Services;
using RestAlpaka.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Neoflix.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // POST: /users
        [HttpPost]
        public async Task<ActionResult<Users>> Create(Users user)
        {
            try
            {
                var createdUser = await _userService.CreateAsync(user);
                return CreatedAtAction(nameof(GetById), new { id = createdUser.UserId }, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id); // Assuming GetByIdAsync is implemented in UserService
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        // DELETE: /users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _userService.DeleteAsync(id);
                if (!success)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return NoContent(); // Successful deletion returns no content
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // PUT: /users/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Users>> Update(int id, Users user)
        {
            if (id != user.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            try
            {
                var updatedUser = await _userService.UpdateAsync(user);
                if (updatedUser == null)
                {
                    return NotFound($"User with ID {id} not found.");
                }
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}