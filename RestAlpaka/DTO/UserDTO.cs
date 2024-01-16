using System.ComponentModel.DataAnnotations;

namespace RestAlpaka.DTO
{
    public class UserDTO
    {

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Username must be between 1 and 50 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email must be up to 100 characters.")]
        public string Email { get; set; }

        [StringLength(20, ErrorMessage = "Role must be up to 20 characters.")]
        public string Role { get; set; }
    }
}
