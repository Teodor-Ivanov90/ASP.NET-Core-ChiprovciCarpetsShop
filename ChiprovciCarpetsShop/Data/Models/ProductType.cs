using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static ChiprovciCarpetsShop.Data.DataConstants.ProductType;

namespace ChiprovciCarpetsShop.Data.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
