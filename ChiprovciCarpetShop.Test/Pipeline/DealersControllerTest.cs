using Xunit;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;
using ChiprovciCarpetsShop.Models.Dealers;
using ChiprovciCarpetsShop.Data.Models;
using System.Linq;

using static ChiprovciCarpetsShop.WebConstants;

namespace ChiprovciCarpetsShop.Test.Pipeline
{
    public class DealersControllerTest
    {
        [Fact]
        public void BecomeShouldBeForAuthorizedUsersAndShoundReturnView()
            => MyMvc
            .Pipeline()
            .ShouldMap(request => request
                .WithPath("/Dealers/Become")
                .WithUser())
            .To<DealersController>(c => c.Become())
                .Which()
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View();

        [Theory]
        [InlineData("Dealer", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndShouldRedirectWithCorrectModel(
           string dealerName,
           string phoneNumber)
           => MyPipeline
            .Configuration()
            .ShouldMap(request => request
                .WithPath("/Dealers/Become")
                .WithMethod(HttpMethod.Post)
            .WithFormFields(new 
            {
                Name = dealerName,
                PhoneNumber = phoneNumber
            })
                .WithUser())
           .To<DealersController>(c => c.Become(new BecomeDealerFormModel 
           {
               Name = dealerName,
               PhoneNumber = phoneNumber
           }))
           .Which()
           .ShouldHave()
           .ActionAttributes(attributes => attributes
               .RestrictingForAuthorizedRequests()
               .RestrictingForHttpMethod(HttpMethod.Post))
           .ValidModelState()
           .Data(data => data.
               WithSet<Dealer>(dealer => dealer
                   .Any(d =>
                       d.Name == dealerName &&
                       d.PhoneNumber == phoneNumber &&
                       d.UserId == TestUser.Identifier)))
           .TempData(tempData => tempData
           .ContainingEntryWithKey(GlobalMessageKey))
           .AndAlso()
           .ShouldReturn()
           .RedirectToAction("All", "Products");
    }
}
