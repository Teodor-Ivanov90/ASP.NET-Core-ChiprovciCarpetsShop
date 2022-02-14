using System;

namespace ChiprovciCarpetsShop.Data
{
    public static class DataConstants
    {
        public static class Product
        {
            public const int ProductModelMinLength = 3;
            public const int ProductModelMaxLength = 30;

            public const int ProductMaterialMinLength = 3;
            public const int ProductMaterialMaxLength = 30;

            public const int ProductMakerMinLength = 3;
            public const int ProductMakerMaxLength = 30;

            public const int ProductMinYear = 1800;
            public const int ProductMaxYear = 2022;
        }

        public static class ProductType
        {
            public const int ProductTypeNameMinLength = 3;
            public const int ProductTypeNameMaxLength = 30;

            
        }
    }
}
