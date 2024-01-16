using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAlpaka.LoginModels;
using RestAlpaka.Model;

namespace RestAlpaka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AlpakaDbContext _dbContext;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AlpakaDbContext dbContext , ILogger<AuthController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            try
            {
                var user = _dbContext.Users.SingleOrDefault(u => u.Username == model.Username);

                if (user != null && passwordHasher.VerifyPassword(model.Password, user.PasswordHash))
                {
                    // Authentication successful
                    // You can generate a JWT token here and return it as part of the response
                    // For simplicity, I'll just return a success message
                    return Ok(new { Message = "Login successful" }); 
                }

                // Authentication failed
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"Exception during login: {ex}");

                // Return a generic error message to the client
                return StatusCode(500, new { Message = "An error occurred during login. Please try again later." });
            }
        }
    


    [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            // Check if the username is already taken
            if (_dbContext.Users.Any(u => u.Username == model.Username))
            {
                return BadRequest(new { Message = "Username is already taken" });
            }
                

            // Hash the password before storing it in the database
            var hashedPassword = passwordHasher.HashPassword(model.Password);

            // Create a new user
            var newUser = new Users
            {
                Username = model.Username,
                PasswordHash = hashedPassword,
                Email = model.Email,
                Role = model.Role
            };

            // Add the user to the database
            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();


            return Ok(new { Message = "User registered successfully" });
        }
    }
}
