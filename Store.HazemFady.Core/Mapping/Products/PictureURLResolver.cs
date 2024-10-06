using AutoMapper;
using AutoMapper.Execution;
using AutoMapper.Internal;
using Microsoft.Extensions.Configuration;
using Store.HazemFady.Core.Dtos.Products;
using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Mapping.Products
{
    public class PictureURLResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration configuration;

        public PictureURLResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string DestMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["BaseURL"]}{source.PictureUrl}";
            }
            return string.Empty ;  
        }
    }
}
