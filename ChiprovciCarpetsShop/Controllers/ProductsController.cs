using ChiprovciCarpetsShop.Infrastructures.Extensions;
using ChiprovciCarpetsShop.Models.Products;
using ChiprovciCarpetsShop.Services.Dealers;
using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChiprovciCarpetsShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService products;
        private readonly IDealerService dealers;

        public ProductsController(
            IProductService products, 
            IDealerService dealers)
        {
            this.products = products;
            this.dealers = dealers;
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myProducts = this.products.ByUser(this.User.Id());

            return View(myProducts);
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
            if (!this.dealers.IsDealer(this.User.Id()))
            {
                return RedirectToAction("Become", "Dealers");
            }
                
            return View(new ProductFormModel
            {
                Types = this.products.GetProductTypes()
            }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ProductFormModel product)
        {
            if (!this.products.IsTypeValid(product.TypeId))
            {
                this.ModelState.AddModelError(nameof(product.TypeId), "The product type is invalid!");
            }    
            
            if (!ModelState.IsValid)
            {
                product.Types = this.products.GetProductTypes();

                return View(product);
            }

            var dealerId = this.dealers.GetId(this.User.Id());

            if (dealerId == 0)
            {
                RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            this.products.Create(product.Model,
                product.Material,
                product.Price,
                product.Maker,
                product.YearOfMade,
                product.TypeId,
                product.ImageUrl,
                dealerId);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.Id();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
               return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var product = this.products.Details(id);

            if (product.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            return View(new ProductFormModel
            {
                Model = product.Model,
                Material = product.Material,
                ImageUrl = product.ImageUrl,
                Maker = product.Maker,
                Price = product.Price,
                YearOfMade = product.Year,
                TypeId =product.TypeId,
                Types = this.products.GetProductTypes()
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, ProductFormModel product)
        {
            if (!this.products.IsTypeValid(product.TypeId))
            {
                this.ModelState.AddModelError(nameof(product.TypeId), "The product type is invalid!");
            }

            if (!ModelState.IsValid)
            {
                product.Types = this.products.GetProductTypes();

                return View(product);
            }

            var dealerId = this.dealers.GetId(this.User.Id());

            if (dealerId == 0 && !User.IsAdmin())
            {
                RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.products.IsByDealer(id,dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            this.products.Edit(
                id,
                product.Model,
                product.Material,
                product.Price,
                product.Maker,
                product.YearOfMade,
                product.TypeId,
                product.ImageUrl);

            return RedirectToAction(nameof(All));

        }
    }
}
