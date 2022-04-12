using ChiprovciCarpetsShop.Services.Products;

namespace ChiprovciCarpetsShop.Infrastructures.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IProductModel product)
            => product.ProductTypeName + "-" + product.Model;
    }
}
