using ChiprovciCarpetsShop.Infrastructures.Extensions;
using ChiprovciCarpetsShop.Models.Dealers;
using ChiprovciCarpetsShop.Services.Dealers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static ChiprovciCarpetsShop.WebConstants;

namespace ChiprovciCarpetsShop.Controllers
{
    public class DealersController : Controller
    {
        private readonly IDealerService dealers;

        public DealersController(IDealerService dealers) => this.dealers = dealers;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become (BecomeDealerFormModel dealer)
        {
            var userId = this.User.Id();

            if (this.dealers.IsDealer(userId))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            this.dealers.Create(dealer.Name,dealer.PhoneNumber, userId);

            TempData[GlobalMessageKey] = "You successfully become a dealer!";

            return RedirectToAction(nameof(ProductsController.All), "Products");
        }
    }
}
