namespace ChiprovciCarpetsShop.Services.Products
{
    public class ProductDetailsServiceModel : ProductServiceModel
    {
        public int TypeId { get; set; }

        public string Material { get; init; }

        public string Maker { get; init; }

        public int YearOfMade { get; init; }

        public int DealerId { get; init; }

        public string UserId { get; init; }
    }
}
