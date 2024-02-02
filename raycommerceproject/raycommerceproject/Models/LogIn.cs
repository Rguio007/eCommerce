using System.ComponentModel.DataAnnotations;

namespace raycommerceproject.Models
{
    public class LogIn
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
