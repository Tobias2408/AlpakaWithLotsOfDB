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
    public class LocationsController : ControllerBase
    {
        private readonly LocationService _locationService;

        public LocationsController(LocationService locationService)
        {
            _locationService = locationService;
        }

        // POST: /locations
        [HttpPost]
        public async Task<ActionResult<Location>> Create(Location location)
        {
            try
            {
                var createdLocation = await _locationService.CreateAsync(location);
                return CreatedAtAction(nameof(GetById), new { id = createdLocation.LocationId }, createdLocation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetAll()
        {
            try
            {
                var locations = await _locationService.GetAllAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /locations/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetById(int id)
        {
            try
            {
                var location = await _locationService.GetByIdAsync(id);
                if (location == null)
                {
                    return NotFound();
                }
                return Ok(location);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: /locations/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Location location)
        {
            if (id != location.LocationId)
            {
                return BadRequest("Location ID mismatch.");
            }

            try
            {
                var updatedLocation = await _locationService.UpdateAsync(location);
                if (updatedLocation == null)
                {
                    return NotFound($"Location with ID {id} not found.");
                }
                return Ok(updatedLocation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /locations/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _locationService.DeleteAsync(id);
                if (!success)
                {
                    return NotFound($"Location with ID {id} not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
