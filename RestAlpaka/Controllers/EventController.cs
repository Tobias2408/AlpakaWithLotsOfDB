using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventManager _eventManager;

        public EventController(AlpakaDbContext context)
        {
            _eventManager = new EventManager(context);

        }


        // GET: api/event
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> Get()
        {
            return Ok(await _eventManager.GetAllAsync());
        }

        // GET: api/event/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> Get(int id)
        {
            var e = await _eventManager.GetByIdAsync(id);
            if (e == null)
            {
                return NotFound();
            }
            return Ok(e);
        }

        // POST: api/event
        [HttpPost]
        public async Task<ActionResult<Event>> Post(Event e)
        {
            await _eventManager.InsertAsync(e);
            return CreatedAtAction(nameof(Get), new { id = e.Event_id }, e); // Assuming 'Id' is the identifier property
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Event e)
        {
            if (id != e.Event_id) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _eventManager.UpdateAsync(e);
            return NoContent();
        }

        // DELETE: api/customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _eventManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }


    }
}
