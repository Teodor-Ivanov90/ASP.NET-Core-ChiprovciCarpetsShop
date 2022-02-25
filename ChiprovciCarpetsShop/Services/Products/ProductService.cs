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
        public bool Edit(
            int productId, 
            string model, 
            string material, 
            decimal price, 
            string maker,
            int yearOfMade, 
            int typeId, 
            string imageUrl)
        {
            var productData = this.data.Products.Find(productId);

            if (productData == null)
            {
                return false;
            }

            productData.Model = model;
            productData.Material = material;
            productData.Price = price;
            productData.Maker = maker;
            productData.YearOfMade = yearOfMade;
            productData.TypeId = typeId;
            productData.ImageUrl = imageUrl;

            this.data.SaveChanges();

            return true;
        }

        public int Create(string model,
            string material, 
            decimal price, 
            string maker, 
            int yearOfMade,
            int typeId, 
            string imageUrl,
            int dealerId)
        {
            var productData = new Product
            {
                Model = model,
                Material = material,
                Price = price,
                Maker = maker,
                YearOfMade = yearOfMade,
                TypeId = typeId,
                ImageUrl = imageUrl,
                DealerId = dealerId
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public ProductDetailsServiceModel Details(int productId)
          => this.data
            .Products
            .Where(p => p.Id == productId)
            .Select(p => new ProductDetailsServiceModel
            {
                Id = p.Id,
                ProductType = p.Type.Name,
                Model = p.Model,
                 Maker = p.Maker,
                 Material = p.Material,
                 ImageUrl = p.ImageUrl,
                 Price = p.Price,
                 Year = p.YearOfMade,
                 DealerId = p.DealerId,
                 UserId = p.Dealer.UserId
            })
            .FirstOrDefault();

        public IEnumerable<string> AllProductTypes()
        {
            return this.data.ProductTypes
                 .Select(pt => pt.Name)
                 .OrderBy(pt => pt)
                 .ToList();
        }

        public bool IsByDealer(int productId, int  dealerId)
         => this.data
            .Products
            .Any(p => p.Id == productId && p.DealerId == dealerId);

        public IEnumerable<ProductServiceModel> ByUser(string userId)
          => this.data
            .Products
            .Where(p => p.Dealer.UserId == userId)
            .Select(p => new ProductServiceModel
            {
                Id = p.Id,
                Model = p.Model,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                ProductType = p.Type.Name
            })
            .ToList();


        public IEnumerable<ProductTypeServiceModel> GetProductTypes()
           =>
            this.data
            .ProductTypes
            .Select(pt => new ProductTypeServiceModel
            {
                Id = pt.Id,
                Name = pt.Name
            })
            .ToList();


        public bool IsTypeValid(int typeId)
            => this.data.ProductTypes.Any(pt => pt.Id == typeId);

        public bool Edit(int id, string model, string material, decimal price, string maker, int yearOfMade, int typeId, string imageUrl, int dealerId)
        {
            throw new System.NotImplementedException();
        }

    }
}
