using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackendWebshop.DTO_s
{
    [Table(name: "User")]
    public class UserDTO
    {

            [Key]
            public int AccountID { get; set; }

            [Column(TypeName = "varchar(500)")]
            public string Username { get; set; } = string.Empty;

            [Column(TypeName = "varchar(500)")]
            public string Email { get; set; } = string.Empty;

            [Column(TypeName = "varchar(500)")]
            public string Password { get; set; } = string.Empty;

        
    }
}
