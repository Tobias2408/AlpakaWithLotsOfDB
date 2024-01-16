using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Managers;
using RestAlpaka.Model;


namespace RestAlpaka.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly ReviewsManager _reviewsManager;

        public ReviewsController(AlpakaDbContext context)
        {
            _reviewsManager = new ReviewsManager(context);

        }


        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviews>>> Get()
        {
            return Ok(await _reviewsManager.GetAllAsync());
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> Get(int id)
        {
            var r = await _reviewsManager.GetByIdAsync(id);
            if (r == null)
            {
                return NotFound();
            }
            return Ok(r);
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Reviews>> Post(Reviews r)
        {
            await _reviewsManager.InsertAsync(r);
            return CreatedAtAction(nameof(Get), new { id = r.Review_id }, r); // Assuming 'Id' is the identifier property
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Reviews r)
        {
            if (id != r.Review_id) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _reviewsManager.UpdateAsync(r);
            return NoContent();
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _reviewsManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
