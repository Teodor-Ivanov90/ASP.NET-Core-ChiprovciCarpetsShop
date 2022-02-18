using System.Collections.Generic;

namespace ChiprovciCarpetsShop.Models.Products
{
    public class AllProductsQueryModel
    {
        public const int ProductsPerPage = 3;

        public string Type { get; init; }

        public IEnumerable<string> Types { get; set; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public int TotalProducts { get; set; }

        public int CurrentPage { get; set; } = 1;


        public IEnumerable<AllProductsViewModel> Products { get; set; }
    }
}
