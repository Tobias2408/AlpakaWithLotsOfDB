using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
      private readonly LocationService _locationService;

        public LocationController(LocationService locationService)
        {
            _locationService = locationService;
        }



        // GET: api/Location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAllLocation()
        {
            var location = await _locationService.GetAllAsync();
            return Ok(location);
        }

        // GET: api/Location/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(string id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        // POST: api/Location
        [HttpPost]
        public async Task<ActionResult<Location>> CreateLocation(Location location)
        {
            await _locationService.CreateAsync(location);
            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(string id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }
            await _locationService.UpdateAsync(id, location);
            return NoContent();
        }

        // DELETE: api/Location/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLocation(string id)
        {
            var location = await _locationService.GetByIdAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            await _locationService.DeleteAsync(id);
            return NoContent();
        }
    }
}
