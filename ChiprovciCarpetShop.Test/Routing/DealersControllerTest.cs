using Xunit;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;
using ChiprovciCarpetsShop.Models.Dealers;

namespace ChiprovciCarpetsShop.Test.Routing
{
     public class DealersControllerTest
    {
        [Fact]
        public void GetBecomeRouteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Dealers/Become")
            .To<DealersController>(c => c.Become());

        [Fact]
        public void PostBecomeRouteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Dealers/Become")
            .WithMethod(HttpMethod.Post))
            .To<DealersController>(c => c.Become(With.Any<BecomeDealerFormModel>()));
    }
}
