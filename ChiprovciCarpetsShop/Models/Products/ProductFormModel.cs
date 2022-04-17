using ChiprovciCarpetsShop.Services.Products;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static ChiprovciCarpetsShop.Data.DataConstants.Product;

namespace ChiprovciCarpetsShop.Models.Products
{
    public class ProductFormModel : IProductModel
    {
        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public string ProductTypeName { get; set; }

        public IEnumerable<ProductTypeServiceModel> Types { get; set; }

        [Required]
        [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
        public string Model { get; set; }

        [Range(0,double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(MaterialMaxLength, MinimumLength = MaterialMinLength)]
        public string Material { get; set; }

        [Required]
        [StringLength(MakerMaxLength, MinimumLength = MakerMinLength)]
        public string Maker { get; set; }

        [Required]
        [Display(Name = "Year of made")]
        [Range(MinYear, MaxYear)]
        public int YearOfMade { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; set; }
    }
}
