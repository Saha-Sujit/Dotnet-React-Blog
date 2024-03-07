using System.ComponentModel.DataAnnotations;

namespace Comment.Models
{
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Comment { get; set; }
    }
}