using ChiprovciCarpetsShop.Models.Products;
using System.Collections.Generic;

namespace ChiprovciCarpetsShop.Models.Api.Products
{
    public class AllProductsApiRequestModel
    {
        public string Type { get; init; }

        public string SearchTerm { get; init; }

        public ProductSorting Sorting { get; init; }

        public int TotalProducts { get; init; }

        public int CurrentPage { get; set; } = 1;

        public int ProductsPerPage { get; init; } = 10;
    }
}
