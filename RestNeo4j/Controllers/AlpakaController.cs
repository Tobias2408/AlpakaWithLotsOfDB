using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Neoflix.Services;
using RestAlpaka.Model;

namespace Neoflix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlpakaController : ControllerBase
    {
        private readonly AlpakaService _alpakaService;

        public AlpakaController(AlpakaService alpakaService)
        {
            _alpakaService = alpakaService;
        }

        // GET: api/Alpaka
        [HttpGet]
        public async Task<IActionResult> GetAllAlpakas()
        {
            var alpakas = await _alpakaService.GetAllAsync();
            return Ok(alpakas);
        }

        // GET: api/Alpaka/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlpaka(int id)
        {
            var alpaka = await _alpakaService.GetAsync(id);
            if (alpaka == null)
            {
                return NotFound();
            }

            return Ok(alpaka);
        }

        // POST: api/Alpaka
        [HttpPost]
        public async Task<IActionResult> CreateAlpaka([FromBody] Alpaka alpaka)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Alpaka createdAlpaka = new();
            try
            {
                 createdAlpaka = await _alpakaService.CreateAsync(alpaka);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            createdAlpaka = await _alpakaService.GetAsync(alpaka.Id);
            return CreatedAtAction(nameof(GetAlpaka), new { id = createdAlpaka.Id }, createdAlpaka);
        }

        // PUT: api/Alpaka/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlpaka(int id, [FromBody] Alpaka alpaka)
        {
            if (id != alpaka.Id)
            {
                return BadRequest();
            }

            await _alpakaService.UpdateAsync(id, alpaka);

            // Assuming you have a Get method to retrieve the updated Alpaka
            var updatedAlpaka = await _alpakaService.GetAsync(id);
            if (updatedAlpaka == null)
            {
                return NotFound();
            }

            return Ok(updatedAlpaka);
        }


        // DELETE: api/Alpaka/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlpaka(int id)
        {
            var alpaka = await _alpakaService.DeleteAsync(id);
            if (!alpaka)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
