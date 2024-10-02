using AutoMapper;
using Store.HazemFady.Core.Dtos.Products;
using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(PD => PD.BrandName, options => options.MapFrom(P => P.Brand.Name))
                .ForMember(PD => PD.TypeName, options => options.MapFrom(P => P.Type.Name));

            CreateMap<ProductBrand, BrandTypeDto>();
            CreateMap<ProductType, BrandTypeDto>();
        }
    }
}
