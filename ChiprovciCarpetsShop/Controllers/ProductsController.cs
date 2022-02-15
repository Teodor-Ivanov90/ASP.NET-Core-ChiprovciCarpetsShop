using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ChiprovciCarpetsShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ChiprovciCapretsDbContext data;

        public ProductsController(ChiprovciCapretsDbContext data)
            => this.data = data;

        public IActionResult All()
        {
            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .Select(p => new AllProductsViewModel
                {
                    Id = p.Id,
                    Model = p.Model,
                    ImageUrl = p.ImageUrl,
                    ProductType = p.Type.Name
                })
                .ToList();

            return View(products);
        }

        public IActionResult Add()
        {
            return View(new AddProductFormModel
            {
                Types = GetProductTypes()
            }) ;
        }

        [HttpPost]
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

            var productData = new Product
            {
                Model = product.Model,
                Material = product.Material,
                Maker = product.Maker,
                YearOfMade = product.YearOfMade,
                TypeId = product.TypeId,
                ImageUrl = product.ImageUrl
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
