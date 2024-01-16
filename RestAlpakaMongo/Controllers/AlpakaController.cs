using Microsoft.AspNetCore.Mvc;
using RestAlpakaMongo.Interfaces;
using RestAlpakaMongo.Models;
using RestAlpakaMongo.Services;

namespace RestAlpakaMongo.Controllers;

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
    public async Task<ActionResult<IEnumerable<Alpaka>>> GetAllAlpakas()
    {
        var alpakas = await _alpakaService.GetAllAsync();
        return Ok(alpakas);
    }

    // GET: api/Alpaka/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Alpaka>> GetAlpaka(string id)
    {
        var alpaka = await _alpakaService.GetByIdAsync(id);
        if (alpaka == null)
        {
            return NotFound();
        }
        return Ok(alpaka);
    }

    // POST: api/Alpaka
    [HttpPost]
    public async Task<ActionResult<Alpaka>> CreateAlpaka(Alpaka alpaka)
    {
        await _alpakaService.CreateAsync(alpaka);
        return CreatedAtAction("GetAlpaka", new { id = alpaka.Id }, alpaka);
    }

    // PUT: api/Alpaka/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAlpaka(string id, Alpaka alpaka)
    {
        if (id != alpaka.Id)
        {
            return BadRequest();
        }
        await _alpakaService.UpdateAsync(id, alpaka);
        return NoContent();
    }

    // DELETE: api/Alpaka/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAlpaka(string id)
    {
        var alpaka = await _alpakaService.GetByIdAsync(id);
        if (alpaka == null)
        {
            return NotFound();
        }
        await _alpakaService.DeleteAsync(id);
        return NoContent();
    }
}
    // You can add more methods here for other operations (POST, PUT, DELETE, etc.)
