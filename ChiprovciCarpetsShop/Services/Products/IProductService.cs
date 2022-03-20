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

        ProductDetailsServiceModel Details(int productId);

        List<ProductServiceModel> Latest();

        bool Edit(
                int productId,
                string model,
                string material,
                decimal price,
                string maker,
                int yearOfMade,
                int typeId,
                string imageUrl);
        int Create(string model,
                string material,
                decimal price,
                string maker,
                int yearOfMade,
                int typeId,
                string imageUrl,
                int dealerId);

        bool IsTypeValid(int typeId);

        IEnumerable<ProductTypeServiceModel> GetProductTypes();

        bool IsByDealer(int productId, int dealerId);
        IEnumerable<ProductServiceModel> ByUser(string userId);
    }
}
