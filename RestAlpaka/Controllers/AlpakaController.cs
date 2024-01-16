using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAlpaka.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestAlpaka.Managers;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlpakaController : ControllerBase
    {
        private readonly AlpakaManager _alpakaManager;

        public AlpakaController(AlpakaDbContext context)
        {
            _alpakaManager = new AlpakaManager(context);
        }

        // GET: api/Alpaka
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alpaka>>> Get()
        {
            return Ok(await _alpakaManager.GetAllAsync());
        }

        // GET: api/Alpaka/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alpaka>> Get(int id)
        {
            var alpaka = await _alpakaManager.GetByIdAsync(id);
            if (alpaka == null)
            {
                return NotFound();
            }
            return alpaka;
        }

        // POST: api/Alpaka
        [HttpPost]
        public async Task<ActionResult<Alpaka>> Post(Alpaka alpaka)
        {
            await _alpakaManager.InsertAsync(alpaka);
            return CreatedAtAction(nameof(Get), new { id = alpaka.alpaka }, alpaka); // Assuming 'Id' is the identifier property
        }

        // PUT: api/Alpaka/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Alpaka alpaka)
        {
            if (id != alpaka.alpaka) // Assuming 'Id' is the identifier property
            {
                return BadRequest();
            }
            await _alpakaManager.UpdateAsync(alpaka);
            return NoContent();
        }

        // DELETE: api/Alpaka/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _alpakaManager.DeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
