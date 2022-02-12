using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChiprovskiKilimi.Data.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
