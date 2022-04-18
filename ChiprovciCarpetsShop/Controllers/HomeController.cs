using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using static ChiprovciCarpetsShop.WebConstants.Cache;

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

            var latestProducts = this.cache.Get<IEnumerable<ProductServiceModel>>(LatestProductsCacheKey);

            if (latestProducts == null || latestProducts.Count() == 0)
            {
                latestProducts = this.products.Latest();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(LatestProductsCacheKey, latestProducts,cacheOptions);
            }

            return View(latestProducts);
        }


        public IActionResult Error() => View();
    }
}
