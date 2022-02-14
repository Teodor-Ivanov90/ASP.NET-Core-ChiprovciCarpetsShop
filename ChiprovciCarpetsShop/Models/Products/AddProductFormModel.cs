using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static ChiprovciCarpetsShop.Data.DataConstants.Product;

namespace ChiprovciCarpetsShop.Models.Products
{
    public class AddProductFormModel
    {
        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public IEnumerable<ProductTypeFormModel> Types { get; set; }

        [Required]
        [StringLength(ProductModelMaxLength, MinimumLength = ProductModelMinLength)]
        public string Model { get; set; }

        [Required]
        [StringLength(ProductMaterialMaxLength, MinimumLength = ProductMaterialMinLength)]
        public string Material { get; set; }

        [Required]
        [StringLength(ProductMakerMaxLength, MinimumLength = ProductMakerMinLength)]
        public string Maker { get; set; }

        [Required]
        [Display(Name = "Year of made")]
        [Range(ProductMinYear, ProductMaxYear)]
        public int YearOfMade { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }
    }
}
