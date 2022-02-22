namespace ChiprovciCarpetsShop.Services.Products
{
    using System.Collections.Generic;
    public class ProductsQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int ProductsPerPage { get; set; }

        public int TotalProducts { get; init; }

        public IEnumerable<ProductServiceModel> Products { get; set; }
    }
}
