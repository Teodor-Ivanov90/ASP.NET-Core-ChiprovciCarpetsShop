using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace ChiprovciCarpetsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChiprovciCarpetsDbContext data;

        public HomeController(ChiprovciCarpetsDbContext data)
            => this.data = data;

        public IActionResult Index()
        {
            var products = this.data
               .Products
               .OrderByDescending(p => p.Id)
               .Select(p => new ProductServiceModel
               {
                   Id = p.Id,
                   Model = p.Model,
                   ImageUrl = p.ImageUrl,
                   ProductType = p.Type.Name
               })
               .Take(3)
               .ToList();

            return View(products);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
