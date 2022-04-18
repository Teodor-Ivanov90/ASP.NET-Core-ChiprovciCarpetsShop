using Xunit;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;
using ChiprovciCarpetsShop.Models.Dealers;
using ChiprovciCarpetsShop.Data.Models;
using System.Linq;

using static ChiprovciCarpetsShop.WebConstants;

namespace ChiprovciCarpetsShop.Test.Controller
{
    public class DealersControllerTest
    {
        [Fact]
        public void GetBecomeShoudBeForAuthorizedUsersAndShouldReturnView()
            => MyController<DealersController>
            .Instance()
            .Calling(c => c.Become())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Dealer","+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndShouldRedirectWithCorrectModel(
            string dealerName,
            string phoneNumber)
            => MyController<DealersController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Become(new BecomeDealerFormModel
            {
                Name = dealerName,
                PhoneNumber = phoneNumber
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests()
                .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .Data(data => data.
                WithSet<Dealer>(dealer => dealer
                    .Any(d=>
                        d.Name == dealerName &&
                        d.PhoneNumber == phoneNumber &&
                        d.UserId == TestUser.Identifier)))
            .TempData(tempData => tempData
            .ContainingEntryWithKey(GlobalMessageKey))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("All","Products");

    }
}
