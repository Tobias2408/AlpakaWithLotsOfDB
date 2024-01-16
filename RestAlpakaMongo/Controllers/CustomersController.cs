using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.DTOs;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersService _customersService;
   

        public CustomersController(CustomersService customersService)
        {
            _customersService = customersService;
         
        }


        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetAllCustomers()
        {
            var Customers = await _customersService.GetAllAsync();
            return Ok(Customers);
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomers(string id)
        {
            var customers = await _customersService.GetByIdAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }

        
        [HttpPost]
        public async Task<ActionResult<Customers>> CreateCustomers([FromBody] CustomerDto customerDto)
        {
            try
            {
                var customer = await _customersService.CreateCustomerAsync(customerDto);
                return CreatedAtAction("GetCustomers", new { id = customer.Id }, customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomers(string id, [FromBody] CustomerDto customerDto)
        {
            try
            {
                await _customersService.UpdateCustomerAsync(id, customerDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomers(string id)
        {
            var customer = await _customersService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            await _customersService.DeleteAsync(id);
            return NoContent();
        }
    }
}
