using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Infrastructures.Extension;
using ChiprovciCarpetsShop.Models.Products;
using ChiprovciCarpetsShop.Services.Dealers;
using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ChiprovciCarpetsShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ChiprovciCarpetsDbContext data;
        private readonly IProductService products;
        private readonly IDealerService dealers;

        public ProductsController(
            ChiprovciCarpetsDbContext data, 
            IProductService products, 
            IDealerService dealers)
        {
            this.data = data;
            this.products = products;
            this.dealers = dealers;
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
            var isUserDealer = this.dealers.UserIsAlreadyDealer(this.User.Id());

            if (!isUserDealer)
            {
                return RedirectToAction("Become", "Dealers");
            }
                
            return View(new AddProductFormModel
            {
                Types = this.products.GetProductTypes()
            }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductFormModel product)
        {
            var isTypeValid = this.products.IsTypeValid(product);

            if (!isTypeValid)
            {
                this.ModelState.AddModelError(nameof(product.TypeId), "The product type is invalid!");
            }    
            
            if (!ModelState.IsValid)
            {
                product.Types = this.products.GetProductTypes();

                return View(product);
            }

            var dealerId = this.dealers.GetId(this.User.Id());
                
            var productData = new Product
            {
                Model = product.Model,
                Material = product.Material,
                Price = product.Price,
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

    }
}
