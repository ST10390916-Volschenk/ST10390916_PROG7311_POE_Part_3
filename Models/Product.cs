using ST10390916_PROG7311_POE.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ST10390916_PROG7311_POE.Models
{
    public enum ProductCategory
    {
        Solar,
        Wind,
        Hydro,
        Crops,
        Livestock,
        Equipment,
        Fertilizers,
        Seeds,
        Pesticides
    }

    public class Product
    {
        public int ProductID { get; set; }
        public int UserID { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public ProductCategory Category { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly ProductionDate { get; set; }

        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? ProductImage { get; set; }

    }
}
