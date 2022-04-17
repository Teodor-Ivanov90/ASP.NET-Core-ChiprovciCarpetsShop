using ChiprovciCarpetsShop.Controllers;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Services.Products;
using ChiprovciCarpetsShop.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ChiprovciCarpetsShop.Test.Controllers
{
    public  class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModel()
        {
            //Arrange
            var data = DatabaseMock.Instance;
            var mapper = MapperMock.Instance;

            var products = Enumerable.Range(0, 10).Select(i => new Product());
            data.Products.AddRange(products);
            data.SaveChanges();

            var productService = new ProductService(data, mapper);

            //var homeController = new HomeController(productService);

            //Act
           // var result = homeController.Index();

            //Assert
           // Assert.NotNull(result);
           // var viewResult = Assert.IsType<ViewResult>(result);
          //  var model = viewResult.Model;
          //  var productServiceModel = Assert.IsType<List<ProductServiceModel>>(model);
         //   Assert.Equal(3, productServiceModel.Count);

        }

        //[Fact]
        //public  void ErrorShouldReturnView()
        //{
        //    //Arrange
        //    var homeController = new HomeController(null);

        //    //Act
        //    var result = homeController.Error();

        //    //Assert
        //    Assert.NotNull(result);
        //    Assert.IsType<ViewResult>(result);
        //}
    }
}
