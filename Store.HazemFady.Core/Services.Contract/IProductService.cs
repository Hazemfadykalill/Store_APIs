using Store.HazemFady.Core.Dtos.Products;
using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Services.Contract
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>> GetAllProductAsync(string?sort);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypeAsync();
        Task<ProductDto> GetProductByIdAsync(int id);

    }
}
