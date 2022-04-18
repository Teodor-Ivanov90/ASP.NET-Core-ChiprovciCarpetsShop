using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Models.Products;
using System.Collections.Generic;

namespace ChiprovciCarpetsShop.Services.Products
{
    public interface IProductService
    {
        ProductsQueryServiceModel All(
            string type = null,
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.DateCreated,
            int currentPage = 1,
            int productsPerPage = int.MaxValue,
            bool publicOnly = true);

        IEnumerable<string> AllProductTypes();

        ProductDetailsServiceModel Details(int productId);

        IEnumerable<ProductServiceModel> Latest();

        void ChangeVisibility(int productId);

        bool Edit(
                int productId,
                string model,
                string material,
                decimal price,
                string maker,
                int yearOfMade,
                int typeId,
                string imageUrl,
                bool IsPublic);
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

        string GetProductTypeName(int id);

        bool IsByDealer(int productId, int dealerId);
        IEnumerable<ProductServiceModel> ByUser(string userId);
    }
}
