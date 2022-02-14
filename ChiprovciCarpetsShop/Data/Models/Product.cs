using System.ComponentModel.DataAnnotations;

using static ChiprovciCarpetsShop.Data.DataConstants.Product;

namespace ChiprovciCarpetsShop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public ProductType Type { get; set; }

        [Required]
        [MaxLength(ProductModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(ProductMaterialMaxLength)]
        public string Material { get; set; }

        [Required]
        [MaxLength(ProductMakerMaxLength)]
        public string Maker { get; set; }

        [Range(ProductMinYear,ProductMaxYear)]
        public int YearOfMade { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
