using Store.HazemFady.Core.Dtos.Products;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Paginations;
using Store.HazemFady.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Services.Contract
{
    public interface IProductService
    {

        Task<PaginationResponse<ProductDto>> GetAllProductAsync(ProductSpecParams productSpecParams);
        Task<IEnumerable<BrandTypeDto>> GetAllBrandAsync();
        Task<IEnumerable<BrandTypeDto>> GetAllTypeAsync();
        Task<ProductDto> GetProductByIdAsync(int id);

    }
}
