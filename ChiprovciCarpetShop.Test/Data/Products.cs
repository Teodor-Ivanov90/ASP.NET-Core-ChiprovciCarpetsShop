using ChiprovciCarpetsShop.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ChiprovciCarpetsShop.Test.Data
{
    public static class Products
    {
        public static IEnumerable<Product> TenPublicProducts
            => Enumerable.Range(0, 10).Select(i => new Product
            {
                IsPublic = true,
                Type = new ProductType { Name = ""}
            });
    }
}
