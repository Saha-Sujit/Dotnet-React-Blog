using System.ComponentModel.DataAnnotations;

namespace Post.Models
{
    public class PostModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Content { get; set; }
        [Required]
        public string? IsPublished { get; set; }
        [Required]
        public string? publishedDate { get; set; }
        [Required]
        public int IsPost { get; set; }
    }
}