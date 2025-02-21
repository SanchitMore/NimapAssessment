using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NimapAssessment.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }

        [Required]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [NotMapped]
        public string? CategoryName { get; set; }
    }
}
