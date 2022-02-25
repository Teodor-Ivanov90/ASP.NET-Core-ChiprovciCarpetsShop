using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Models.Products;
using System.Collections.Generic;
using System.Linq;

namespace ChiprovciCarpetsShop.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ChiprovciCarpetsDbContext data;

        public ProductService(ChiprovciCarpetsDbContext data)
            => this.data = data;

        public ProductsQueryServiceModel All(
            string type,
            string searchTerm,
            ProductSorting sorting,
            int currentPage,
            int productsPerPage)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(type))
            {
                productsQuery = productsQuery
                    .Where(p =>
                    p.Type.Name == type);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery
                    .Where(p =>
                    p.Model.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Maker.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Type.Name.ToLower().Contains(searchTerm.ToLower())
                    );
            }

            productsQuery = sorting switch
            {
                ProductSorting.Model => productsQuery.OrderBy(p => p.Model),
                ProductSorting.Type => productsQuery.OrderBy(p => p.Type.Name),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductServiceModel
                {
                    Id = p.Id,
                    Model = p.Model,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    ProductType = p.Type.Name,

                })
                .ToList();

            return new ProductsQueryServiceModel
            {
                CurrentPage = currentPage,
                ProductsPerPage = productsPerPage,
                TotalProducts = totalProducts,
                Products = products
            };
        }



        public IEnumerable<string> AllProductTypes()
        {
            return this.data.ProductTypes
                 .Select(pt => pt.Name)
                 .OrderBy(pt => pt)
                 .ToList();
        }

        public IEnumerable<ProductTypeFormModel> GetProductTypes()
           =>
            this.data
            .ProductTypes
            .Select(pt => new ProductTypeFormModel
            {
                Id = pt.Id,
                Name = pt.Name
            })
            .ToList();


        public bool IsTypeValid(AddProductFormModel product)
            => !this.data.ProductTypes.Any(pt => pt.Id == product.TypeId);
    }
}
