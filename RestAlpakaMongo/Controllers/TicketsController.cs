using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
       private readonly TicketsService _ticketsService;

        public TicketsController(TicketsService ticketsService)
        {
            _ticketsService = ticketsService;
        }


        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tickets>>> GetAllTickets()
        {
            var tickets = await _ticketsService.GetAllAsync();
            return Ok(tickets);
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tickets>> GetTickets(string id)
        {
            var tickets = await _ticketsService.GetByIdAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            return Ok(tickets);
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Tickets>> CreateTickets(Tickets tickets)
        {
            await _ticketsService.CreateAsync(tickets);
            return CreatedAtAction("GetTickets", new { id = tickets.Id }, tickets);
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTickets(string id, Tickets tickets)
        {
            if (id != tickets.Id)
            {
                return BadRequest();
            }
            await _ticketsService.UpdateAsync(id,tickets);
            return NoContent();
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTickets(string id)
        {
            var tickets = await _ticketsService.GetByIdAsync(id);
            if (tickets == null)
            {
                return NotFound();
            }
            await _ticketsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
