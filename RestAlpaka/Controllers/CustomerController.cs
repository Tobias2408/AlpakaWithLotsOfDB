using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerManager _customerManager;

        public CustomerController(AlpakaDbContext context)
        {
            _customerManager = new CustomerManager(context);

        }


        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> Get()
        {
            return Ok(await _customerManager.GetAllAsync());
        }

        // GET: api/customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> Get(int id)
        {
            var customer = await _customerManager.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customers>> Post(Customers customers)
        {
            await _customerManager.InsertAsync(customers);
            return CreatedAtAction(nameof(Get), new { id = customers.Customer_id }, customers); // Assuming 'Id' is the identifier property
        }

        // PUT: api/customer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Customers customers)
        {
            if (id != customers.Customer_id) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _customerManager.UpdateAsync(customers);
            return NoContent();
        }

        // DELETE: api/customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _customerManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }





    }
}
