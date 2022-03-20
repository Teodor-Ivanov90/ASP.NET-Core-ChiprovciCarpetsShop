using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace ChiprovciCarpetsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService products;

        public HomeController(IProductService products)
            => this.products = products;

        public IActionResult Index()
        {
            var products = this.products.Latest();

            return View(products);
        }


        public IActionResult Error() => View();
    }
}
