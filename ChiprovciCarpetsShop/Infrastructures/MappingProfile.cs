using AutoMapper;
using ChiprovciCarpetsShop.Data.Models;
using ChiprovciCarpetsShop.Models.Products;
using ChiprovciCarpetsShop.Services.Products;

namespace ChiprovciCarpetsShop.Infrastructures
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<ProductDetailsServiceModel, ProductFormModel>();

            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.ProductTypeName, cfg => cfg.MapFrom(p => p.Type.Name))
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Dealer.UserId));

            this.CreateMap<Product, ProductServiceModel>()
                .ForMember(p => p.ProductTypeName, cfg => cfg.MapFrom(p => p.Type.Name));

            this.CreateMap<ProductType, ProductTypeServiceModel>();
        }
    }
}
