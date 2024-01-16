using Microsoft.AspNetCore.Mvc;
using Neo4j.Driver;

namespace Neoflix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IDriver _neo4jDriver;

        public StatusController(IDriver neo4jDriver)
        {
            _neo4jDriver = neo4jDriver;
        }

        /// <summary>
        /// Get some basic information about the status of the API, including whether the Neo4j Driver has been defined.
        /// This is for debugging purposes only.
        /// </summary>
        /// <returns>Http Result</returns>
        [HttpGet]
        public IActionResult GetAsync()
        {
            var result = new
            {
                driverInitialized = _neo4jDriver != null
            };
            return Ok(result);
        }
    }
}