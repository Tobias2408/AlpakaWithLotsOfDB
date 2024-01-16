using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationManager _locationManager;

        public LocationController(AlpakaDbContext context)
        {
            _locationManager = new LocationManager(context);

        }


        // GET: api/location
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> Get()
        {
            return Ok(await _locationManager.GetAllAsync());
        }

        // GET: api/location/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> Get(int id)
        {
            var e = await _locationManager.GetByIdAsync(id);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }

        // POST: api/Location
        [HttpPost]
        public async Task<ActionResult<Location>> Post(Location l)
        {
            await _locationManager.InsertAsync(l);
            return CreatedAtAction(nameof(Get), new { id = l.Location_id }, l); // Assuming 'Id' is the identifier property
        }

        // PUT: api/location/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Location l)
        {
            if (id != l.Location_id) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _locationManager.UpdateAsync(l);
            return NoContent();
        }

        // DELETE: api/location/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _locationManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
