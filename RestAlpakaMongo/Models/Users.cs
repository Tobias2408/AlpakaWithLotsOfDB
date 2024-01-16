using RestAlpakaMongo.GenericBase;
using System.ComponentModel.DataAnnotations;

namespace RestAlpakaMongo.Models
{
    public class Users : BaseEntity
    {
      

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
