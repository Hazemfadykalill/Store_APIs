using AutoMapper;
using Microsoft.Extensions.Configuration;
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
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(PD => PD.BrandName, options => options.MapFrom(P => P.Brand.Name))
                .ForMember(PD => PD.TypeName, options => options.MapFrom(P => P.Type.Name))
                //.ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BaseURL"]}{s.PictureUrl}"))
                // Or Equal
                .ForMember(d => d.PictureUrl, options => options.MapFrom(new PictureURLResolver(configuration)));

                ;

            CreateMap<ProductBrand, BrandTypeDto>();
            CreateMap<ProductType, BrandTypeDto>();
        }

     
    }
}
