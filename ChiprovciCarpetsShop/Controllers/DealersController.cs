using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Infrastructures.Extension;
using ChiprovciCarpetsShop.Models.Dealers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChiprovciCarpetsShop.Controllers
{
    public class DealersController : Controller
    {
        private readonly ChiprovciCarpetsDbContext data;

        public DealersController(ChiprovciCarpetsDbContext data) 
            => this.data = data;

        [Authorize]
        public IActionResult Become() => View();

        [HttpPost]
        [Authorize]
        public IActionResult Become (BecomeDealerFormModel dealer)
        {
            var userId = this.User.Id();

            var userIsAlreadyDealer = this.data
                .Dealers
                .Any(d => d.UserId == userId);

            if (userIsAlreadyDealer)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            return RedirectToAction(nameof(ProductsController.All), "Products");
        }
    }
}
