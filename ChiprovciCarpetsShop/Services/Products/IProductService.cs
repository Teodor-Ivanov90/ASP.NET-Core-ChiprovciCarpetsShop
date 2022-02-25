using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Models.Products;
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

        bool IsTypeValid(AddProductFormModel product);

        IEnumerable<ProductTypeFormModel> GetProductTypes();
    }
}
