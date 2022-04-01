using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace ChiprovciCarpetsShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService products;
        private readonly IMemoryCache cache;

        public HomeController(IProductService products, IMemoryCache cache)
        {
            this.products = products;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            const string latestProductsCacheKey = "LatestProductsCacheKey";

            var latestProducts = this.cache.Get<List<ProductServiceModel>>(latestProductsCacheKey);

            if (latestProducts == null)
            {
                latestProducts = this.products.Latest();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestProductsCacheKey, latestProducts,cacheOptions);
            }

            return View(products);
        }


        public IActionResult Error() => View();
    }
}
