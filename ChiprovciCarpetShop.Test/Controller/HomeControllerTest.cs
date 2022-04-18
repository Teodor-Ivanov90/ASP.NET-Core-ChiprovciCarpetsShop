using Xunit;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;

using static ChiprovciCarpetsShop.Test.Data.Products;
using static ChiprovciCarpetsShop.WebConstants.Cache;
using System.Collections;
using ChiprovciCarpetsShop.Services.Products;
using System.Collections.Generic;
using FluentAssertions;
using System;

namespace ChiprovciCarpetsShop.Test.Controller
{
     public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
            => MyController<HomeController>
            .Instance(instance => instance
                .WithData(TenPublicProducts))
            .Calling(c => c.Index())
            .ShouldHave()
              .MemoryCache(cache => cache
                    .ContainingEntry(entry => entry
                        .WithKey(LatestProductsCacheKey)
                        .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                        .WithValueOfType<List<ProductServiceModel>>()))
                .AndAlso()
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<List<ProductServiceModel>>()
            .Passing(model => model.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Error())
            .ShouldReturn()
            .View();
    }
}
