using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentsService _paymentsService;

        public PaymentsController(PaymentsService paymentsService)
        {
            _paymentsService = paymentsService;
        }


        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payments>>> GetAllPayments()
        {
            var payments = await _paymentsService.GetAllAsync();
            return Ok(payments);
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payments>> GetPayments(string id)
        {
            var payments = await _paymentsService.GetByIdAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            return Ok(payments);
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<Payments>> CreateLocation(Payments payments)
        {
            await _paymentsService.CreateAsync(payments);
            return CreatedAtAction("GetPayments", new { id = payments.Id }, payments);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayments(string id, Payments payments)
        {
            if (id != payments.Id)
            {
                return BadRequest();
            }
            await _paymentsService.UpdateAsync(id, payments);
            return NoContent();
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePayments(string id)
        {
            var payments = await _paymentsService.GetByIdAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            await _paymentsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
