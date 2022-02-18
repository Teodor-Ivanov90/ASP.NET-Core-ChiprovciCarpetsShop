using System.ComponentModel.DataAnnotations;

using static ChiprovciCarpetsShop.Data.DataConstants.Product;

namespace ChiprovciCarpetsShop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        [Required]
        public ProductType Type { get; set; }

        [Required]
        [MaxLength(ModelMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(MaterialMaxLength)]
        public string Material { get; set; }

        [Required]
        [MaxLength(MakerMaxLength)]
        public string Maker { get; set; }

        [Range(MinYear,MaxYear)]
        public int YearOfMade { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int DealerId { get; init; }

        public Dealer Dealer { get; init; }
    }
}
