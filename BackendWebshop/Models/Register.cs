using System.ComponentModel.DataAnnotations;

namespace BackendWebshop.Models
{
    public class Register
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public virtual string Email { get; set; }
        [Required]
        public virtual string Password { get; set; }
    }
}
