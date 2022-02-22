using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Infrastructures.Extension;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Models.Products;
using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace ChiprovciCarpetsShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ChiprovciCarpetsDbContext data;
        private readonly IProductService products;

        public ProductsController(ChiprovciCarpetsDbContext data, IProductService products)
        {
            this.data = data;
            this.products = products;
        }

        public IActionResult All([FromQuery] AllProductsQueryModel query)
        {
            var queryResult = this.products.All(
                query.Type, 
                query.SearchTerm, 
                query.Sorting, 
                query.CurrentPage,
                AllProductsQueryModel.ProductsPerPage);

            var productTypes = this.products.AllProductTypes();

            query.Types = productTypes;
            query.Products = queryResult.Products;
            query.TotalProducts = queryResult.TotalProducts;

            return View(query);
        }

        [Authorize]
        public IActionResult Add()
        {
            var isUserDealer = this.data
                .Dealers
                .Any(d => d.UserId == this.User.Id());

            if (!isUserDealer)
            {
                return RedirectToAction("Become", "Dealers");
            }
                
            return View(new AddProductFormModel
            {
                Types = GetProductTypes()
            }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductFormModel product)
        {
            if (!this.data.ProductTypes.Any(pt => pt.Id == product.TypeId))
            {
                this.ModelState.AddModelError(nameof(product.TypeId), "The product type is invalid!");
            }

            if (!ModelState.IsValid)
            {
                product.Types = this.GetProductTypes();

                return View(product);
            }

            var dealerId = this.data.Dealers
                .Where(d => d.UserId == this.User.Id())
                .Select(d => d.Id)
                .FirstOrDefault();
                

            var productData = new Product
            {
                Model = product.Model,
                Material = product.Material,
                Maker = product.Maker,
                YearOfMade = product.YearOfMade,
                TypeId = product.TypeId,
                ImageUrl = product.ImageUrl,
                DealerId = dealerId
            };

            this.data.Products.Add(productData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));
        }

        private IEnumerable<ProductTypeFormModel> GetProductTypes()
            => this.data
            .ProductTypes
            .Select(pt => new ProductTypeFormModel
            {
                Id = pt.Id,
                Name = pt.Name
            })
            .ToList();
    }
}
