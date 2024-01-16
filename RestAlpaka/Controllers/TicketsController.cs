using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly TicketsManager _tickitsManager;

        public TicketsController(AlpakaDbContext context)
        {
            _tickitsManager = new TicketsManager(context);

        }


        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tickets>>> Get()
        {
            return Ok(await _tickitsManager.GetAllAsync());
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tickets>> Get(int id)
        {
            var t = await _tickitsManager.GetByIdAsync(id);
            if (t == null)
            {
                return NotFound();
            }
            return Ok(t);
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Tickets>> Post(Tickets t)
        {
            await _tickitsManager.InsertAsync(t);
            return CreatedAtAction(nameof(Get), new { id = t.TicketNumber }, t); // Assuming 'Id' is the identifier property
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tickets t)
        {
            if (id != t.TicketNumber) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _tickitsManager.UpdateAsync(t);
            return NoContent();
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _tickitsManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
