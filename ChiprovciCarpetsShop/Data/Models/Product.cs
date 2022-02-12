using System.ComponentModel.DataAnnotations;

namespace ChiprovskiKilimi.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public ProductType Type { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Material { get; set; }

        [Required]
        public string Maker { get; set; }

        public int YearOfMade { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
