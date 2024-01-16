using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
       private readonly ReviewsService _reviewsService;

        public ReviewsController(ReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }


        // GET: api/Reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetAllReviews()
        {
            var reviews = await _reviewsService.GetAllAsync();
            return Ok(reviews);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> GetReviews(string id)
        {
            var reviews = await _reviewsService.GetByIdAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            return Ok(reviews);
        }

        // POST: api/Reviews
        [HttpPost]
        public async Task<ActionResult<Reviews>> CreateReviews(Reviews reviews)
        {
            await _reviewsService.CreateAsync(reviews);
            return CreatedAtAction("GetReviews", new { id = reviews.Id }, reviews);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReviews(string id, Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return BadRequest();
            }
            await _reviewsService.UpdateAsync(id, reviews);
            return NoContent();
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReviews(string id)
        {
            var reviews = await _reviewsService.GetByIdAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }
            await _reviewsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
