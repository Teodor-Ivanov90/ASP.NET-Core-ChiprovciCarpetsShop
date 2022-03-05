using Microsoft.AspNetCore.Mvc;

namespace ChiprovciCarpetsShop.Areas.Admin.Controllers
{ 
    public class ProductsController : AdminController
    {
        public IActionResult Index() => View();
    }
}
