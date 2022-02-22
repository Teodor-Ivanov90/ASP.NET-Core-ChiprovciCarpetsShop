using ChiprovciCarpetsShop.Data;
using ChiprovciCarpetsShop.Models;
using ChiprovciCarpetsShop.Models.Api.Products;
using ChiprovciCarpetsShop.Models.Products;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ChiprovciCarpetsShop.Controllers.Api
{
    [ApiController]
    [Route("/api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly ChiprovciCapretsDbContext data;

        public ProductsApiController(ChiprovciCapretsDbContext data) 
            => this.data = data;

        [HttpGet]
        public ActionResult<AllProductsApiResponseModel> All([FromQuery] AllProductsApiRequestModel query)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Type))
            {
                productsQuery = productsQuery
                    .Where(p =>
                    p.Type.Name == query.Type);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery
                    .Where(p =>
                    p.Model.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    p.Maker.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    p.Type.Name.ToLower().Contains(query.SearchTerm.ToLower())
                    );
            }

            productsQuery = query.Sorting switch
            {
                ProductSorting.Model => productsQuery.OrderBy(p => p.Model),
                ProductSorting.Type => productsQuery.OrderBy(p => p.Type.Name),
                ProductSorting.DateCreated or _ => productsQuery.OrderByDescending(p => p.Id)
            };

            var totalProducts = productsQuery.Count();

            var products = productsQuery
                .Skip((query.CurrentPage - 1) * query.ProductsPerPage)
                .Take(query.ProductsPerPage)
                .Select(p => new ProductResponseModel
                {
                    Id = p.Id,
                    Model = p.Model,
                    ImageUrl = p.ImageUrl,
                    ProductType = p.Type.Name,

                })
                .ToList();


            return new AllProductsApiResponseModel
            {
                CurrentPage = query.CurrentPage,
                ProductsPerPage = query.ProductsPerPage,
                TotalProducts = totalProducts,
                Products = products
            };
        }

    }
}
