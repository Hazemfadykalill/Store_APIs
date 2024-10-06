using AutoMapper;
using Store.HazemFady.Core;
using Store.HazemFady.Core.Dtos.Products;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Services.Contract;
using Store.HazemFady.Core.Specifications;
using Store.HazemFady.Core.Specifications.Products;




namespace Store.HazemFady.Services.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async  Task<IEnumerable<ProductDto>> GetAllProductAsync()
        {
            var WithSpec = new ProductSpecification();

            return mapper.Map<IEnumerable<ProductDto>>(await unitOfWork.Repository< Product, int>().GetAllWithSpecAsync(WithSpec));
        }  
        public async  Task<ProductDto> GetProductByIdAsync(int id)
        {

            var WithSpec = new ProductSpecification(id);

            return mapper.Map<ProductDto>(await unitOfWork.Repository<Product, int>().GetWithSpecAsync(WithSpec));
        }
        public async  Task<IEnumerable<BrandTypeDto>> GetAllTypeAsync()
        {
            return mapper.Map<IEnumerable<BrandTypeDto>>(await unitOfWork.Repository<ProductType, int>().GetAllAsync());
             

        }
        public async  Task<IEnumerable<BrandTypeDto>> GetAllBrandAsync()
        {
            return mapper.Map<IEnumerable<BrandTypeDto>>(await unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }



    }
}
