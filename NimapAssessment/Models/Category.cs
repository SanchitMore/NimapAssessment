using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NimapAssessment.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string? CategoryName { get; set; }
    }
}
