using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Models.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ChiprovciCarpetsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChiprovciCapretsDbContext data;

        public HomeController(ChiprovciCapretsDbContext data)
            => this.data = data;

        public IActionResult Index()
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
