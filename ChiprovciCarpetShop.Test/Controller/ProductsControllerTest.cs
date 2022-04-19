using Xunit;
using MyTested.AspNetCore.Mvc;
using ChiprovciCarpetsShop.Controllers;
using ChiprovciCarpetsShop.Services.Products;
using System.Collections.Generic;
using ChiprovciCarpetsShop.Data.Models;

namespace ChiprovciCarpetsShop.Test.Controller
{
    public class ProductsControllerTest
    {
        [Fact]
        public void MineShouldReturnViewWithCorrectModel()
            => MyController<ProductsController>
            .Instance(controller => controller
            .WithUser())
            .Calling(c => c.Mine())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<List<ProductServiceModel>>());

        [Theory]
        [InlineData(1, "Carpet-Kanatica")]
        public void DetailsShouldReturnVievWithCorretModelAndData(int id, string information)
            => MyController<ProductsController>
            .Instance(controller => controller
                .WithData(new Product
                {
                    Id =1,
                    ImageUrl ="",
                    IsPublic = true,
                    Maker = "",
                    Material = "",
                    Model = "Kanatica",
                    Price = 10,
                    YearOfMade = 2000,
                     Type = new ProductType { Id=1, Name ="Carpet"},
                     Dealer = new Dealer { Id=1,UserId = TestUser.Identifier}
                }))
            .Calling(c => c.Details(id, information))
            .ShouldReturn()
            .View(view => view
                .WithModelOfType<ProductDetailsServiceModel>());

        [Theory]
        [InlineData(1, "Carpet")]
        public void DetailsShouldReturnBadRequestWhenInformationDoesNotMatch(int id, string information)
           => MyController<ProductsController>
           .Instance(controller => controller
               .WithData(new Product
               {
                   Id = 1,
                   ImageUrl = "",
                   IsPublic = true,
                   Maker = "",
                   Material = "",
                   Model = "Kanatica",
                   Price = 10,
                   YearOfMade = 2000,
                   Type = new ProductType { Id = 1, Name = "Carpet" },
                   Dealer = new Dealer { Id = 1, UserId = TestUser.Identifier }
               }))
           .Calling(c => c.Details(id, information))
           .ShouldReturn()
           .BadRequest();
    }
}
