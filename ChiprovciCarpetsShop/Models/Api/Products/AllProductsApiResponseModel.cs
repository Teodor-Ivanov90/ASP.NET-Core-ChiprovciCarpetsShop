using System.Collections.Generic;

namespace ChiprovciCarpetsShop.Models.Api.Products
{
    public class AllProductsApiResponseModel
    {
        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; init; }

        public IEnumerable<ProductResponseModel> Products { get; set; }
    }
}
