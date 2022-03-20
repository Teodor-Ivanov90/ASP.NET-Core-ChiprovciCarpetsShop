using AutoMapper;
using ChiprovciCarpetsShop.Infrastructures;

namespace ChiprovciCarpetsShop.Test.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
