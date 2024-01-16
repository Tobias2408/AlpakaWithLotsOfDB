using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var Event = await _eventService.GetAllAsync();
            return Ok(Event);
        }

        // GET: api/Event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(string id)
        {
            var e = await _eventService.GetByIdAsync(id);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }

        // POST: api/Event
        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event e)
        {
            await _eventService.CreateAsync(e);
            return CreatedAtAction("GetEvent", new { id = e.Id }, e);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(string id, Event e)
        {
            if (id != e.Id)
            {
                return BadRequest();
            }
            await _eventService.UpdateAsync(id, e);
            return NoContent();
        }

        // DELETE: api/Event/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEvent(string id)
        {
            var e = await _eventService.GetByIdAsync(id);
            if (e == null)
            {
                return NotFound();
            }
            await _eventService.DeleteAsync(id);
            return NoContent();
        }
    }
}
