using Xunit;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;

namespace ChiprovciCarpetsShop.Test.Routing
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShoulBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShoulBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error());
    }
}
