using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper mapper;

        public ProductService(
            ChiprovciCarpetsDbContext data,
            IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        } 

        public ProductsQueryServiceModel All(
           string type = null,
            string searchTerm = null,
            ProductSorting sorting = ProductSorting.DateCreated,
            int currentPage=1,
            int productsPerPage = int.MaxValue,
            bool publicOnly = true)
        {
            var productsQuery = this.data.Products
                .Where(p => publicOnly ? p.IsPublic : true);

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
                .ProjectTo<ProductServiceModel>(this.mapper.ConfigurationProvider)
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
            string imageUrl,
            bool isPublic)
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
            productData.IsPublic = isPublic;

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
                DealerId = dealerId,
                IsPublic = false
            };

            this.data.Products.Add(productData);
            this.data.SaveChanges();

            return productData.Id;
        }

        public void ChangeVisibility(int productId)
        {
            var product = this.data.Products.Find(productId);

            product.IsPublic = !product.IsPublic;

            this.data.SaveChanges();
        }

        public ProductDetailsServiceModel Details(int productId)
          => this.data
            .Products
            .Where(p => p.Id == productId)
            .ProjectTo<ProductDetailsServiceModel>(this.mapper.ConfigurationProvider)
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
            .ProjectTo<ProductServiceModel>(this.mapper.ConfigurationProvider)
            .ToList();


        public IEnumerable<ProductTypeServiceModel> GetProductTypes()
           =>
            this.data
            .ProductTypes
            .ProjectTo<ProductTypeServiceModel>(this.mapper.ConfigurationProvider)
            .ToList();


        public bool IsTypeValid(int typeId)
            => this.data.ProductTypes.Any(pt => pt.Id == typeId);


        public IEnumerable<ProductServiceModel> Latest() 
            => this.data
               .Products
               .Where(p => p.IsPublic)
               .OrderByDescending(p => p.Id)
               .ProjectTo<ProductServiceModel>(this.mapper.ConfigurationProvider)
               .Take(3)
               .ToList();

        public string GetProductTypeName(int id)
            => this.data.ProductTypes
                .Where(t => t.Id == id)
                .Select(pt => pt.Name)
                .FirstOrDefault();

    }
}
