using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestAlpaka.Model
{
    public class Users
    {
   
        public int User_id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
        
        public string PasswordHash { get; set; } 

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
