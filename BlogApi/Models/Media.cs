using System.ComponentModel.DataAnnotations;

namespace Media.Models
{
    public class MediaModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string? ImagePath { get; set; }
    }
}