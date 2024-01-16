using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingManager _bookingManager;

        public BookingController(AlpakaDbContext context)
        {
            _bookingManager = new BookingManager(context); 
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookings>>> Get()
        {
            return Ok(await _bookingManager.GetAllAsync());
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookings>> Get(int id)
        {
            var booking = await _bookingManager.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return booking;
        }

        // POST: api/Booking
        //[HttpPost]
        //public async Task<ActionResult<Bookings>> Post(Bookings bookings)
        //{
        //    await _bookingManager.InsertAsync(bookings);
        //    return CreatedAtAction(nameof(Get), new { id = bookings.Booking_id }, bookings); // Assuming 'Id' is the identifier property
        //}

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Bookings newBooking)
        {
            if (newBooking == null)
            {
                return BadRequest("Invalid booking data");
            }

            await _bookingManager.InsertAsync(newBooking);

            // You might want to return a different status code or additional information depending on your requirements
            return CreatedAtAction(nameof(Get), new { id = newBooking.Booking_id }, newBooking);
        }

        // PUT: api/bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Bookings bookings)
        {
            if (id != bookings.Booking_id) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _bookingManager.UpdateAsync(bookings);
            return NoContent();
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _bookingManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
