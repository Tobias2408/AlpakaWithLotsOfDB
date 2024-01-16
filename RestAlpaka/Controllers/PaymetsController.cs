using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymetsController : ControllerBase
    {
        private readonly PaymentsManager _paymentsManager;

        public PaymetsController(AlpakaDbContext context)
        {
            _paymentsManager = new PaymentsManager(context);

        }


        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payments>>> Get()
        {
            return Ok(await _paymentsManager.GetAllAsync());
        }

        // GET: api/payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payments>> Get(int id)
        {
            var p = await _paymentsManager.GetByIdAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            return Ok(p);
        }

        // POST: api/payments
        [HttpPost]
        public async Task<ActionResult<Payments>> Post(Payments p)
        {
            await _paymentsManager.InsertAsync(p);
            return CreatedAtAction(nameof(Get), new { id = p.Payment_id }, p); // Assuming 'Id' is the identifier property
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Payments p)
        {
            if (id != p.Payment_id) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _paymentsManager.UpdateAsync(p);
            return NoContent();
        }

        // DELETE: api/location/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _paymentsManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
