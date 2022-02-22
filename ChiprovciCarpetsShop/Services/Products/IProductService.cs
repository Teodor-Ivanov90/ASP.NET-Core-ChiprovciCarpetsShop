using ChiprovciCarpetsShop.Models;
using System.Collections.Generic;

namespace ChiprovciCarpetsShop.Services.Products
{
    public interface IProductService
    {
        ProductsQueryServiceModel All(
            string type,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage);

        IEnumerable<string> AllProductTypes();
    }
}
