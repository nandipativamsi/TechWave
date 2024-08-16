using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TechWave.Models.DomainModel
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [MaxLength(500, ErrorMessage = "Brand cannot exceed 500 characters.")]
        public string Brand { get; set; }

        [MaxLength(2000, ErrorMessage = "Long description cannot exceed 2000 characters.")]
        public string LongDescription { get; set; }

        public string Specifications { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey("Category")]
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public string ImageURL { get; set; }
    }
}
