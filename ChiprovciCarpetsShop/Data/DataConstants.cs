using System;

namespace ChiprovciCarpetsShop.Data
{
    public static class DataConstants
    {
        public static class Product
        {
            public const int ModelMinLength = 3;
            public const int ModelMaxLength = 30;

            public const int MaterialMinLength = 3;
            public const int MaterialMaxLength = 30;

            public const int MakerMinLength = 3;
            public const int MakerMaxLength = 30;

            public const int MinYear = 1800;
            public const int MaxYear = 2022;
        }

        public static class Dealer
        {
            public const int NameMaxLength = 20;

            public const int PhoneNumberMaxLength = 30;
        }
        public static class ProductType
        {
            public const int TypeNameMinLength = 3;
            public const int TypeNameMaxLength = 30;

            
        }
    }
}
