using System.ComponentModel.DataAnnotations;

namespace Category.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}