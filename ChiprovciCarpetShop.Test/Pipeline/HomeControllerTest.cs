using Xunit;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;
using System;
using ChiprovciCarpetsShop.Services.Products;
using System.Collections.Generic;

using static ChiprovciCarpetsShop.Test.Data.Products;
using static ChiprovciCarpetsShop.WebConstants.Cache;

namespace ChiprovciCarpetsShop.Test.Pipeline
{
    public  class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
              => MyMvc
                .Pipeline()
                .ShouldMap("/")
                .To<HomeController>(c => c.Index())
                .Which(controller => controller
                    .WithData(TenPublicProducts))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<List<ProductServiceModel>>()
                    .Passing(m => m.Should().HaveCount(0)));

    }
}
