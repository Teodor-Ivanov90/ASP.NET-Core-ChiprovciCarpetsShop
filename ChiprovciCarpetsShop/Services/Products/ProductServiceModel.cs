namespace ChiprovciCarpetsShop.Services.Products
{
    public class ProductServiceModel : IProductModel
    {
        public int Id { get; set; }

        public string ProductTypeName { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsPublic { get; set; }


    }
}
