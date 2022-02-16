using System.Collections.Generic;

namespace ChiprovciCarpetsShop.Models.Products
{
    public class AllProductsQueryModel
    {
        public string Type { get; init; }
        public IEnumerable<string> Types { get; set; }
        public string SearchTerm { get; init; }
        public ProductSorting Sorting { get; init; }
        public IEnumerable<AllProductsViewModel> Products { get; set; }
    }
}
