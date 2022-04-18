using Xunit;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;

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
    }
}
