using System.ComponentModel.DataAnnotations;

namespace Users.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? password { get; set; }
    }

    public class UserLogin
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? password { get; set; }
    }
}