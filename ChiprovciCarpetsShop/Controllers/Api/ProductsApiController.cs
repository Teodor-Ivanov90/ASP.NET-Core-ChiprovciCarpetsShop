using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Models.Api.Products;
using ChiprovciCarpetsShop.Models.Products;
using ChiprovciCarpetsShop.Services.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChiprovciCarpetsShop.Controllers.Api
{
    [ApiController]
    [Route("/api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly IProductService products;

        public ProductsApiController(IProductService products) 
            => this.products = products;

        [HttpGet]
        public ActionResult<AllProductsApiResponseModel> All([FromQuery] AllProductsApiRequestModel query)
        {
            var productsQuery = this.products.All(
                query.Type,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.ProductsPerPage);


            return new AllProductsApiResponseModel
            {
                CurrentPage = query.CurrentPage,
                ProductsPerPage = query.ProductsPerPage,
                TotalProducts = productsQuery.TotalProducts,
                Products = productsQuery.Products
            };
        }

    }
}
