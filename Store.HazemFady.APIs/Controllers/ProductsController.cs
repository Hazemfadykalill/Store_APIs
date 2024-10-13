using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.APIs.Attributes;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.Core.Dtos.Products;
using Store.HazemFady.Core.Paginations;
using Store.HazemFady.Core.Services.Contract;
using Store.HazemFady.Core.Specifications.Products;

namespace Store.HazemFady.APIs.Controllers
{

    public class ProductsController : BaseAPIController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet("AllProduct")]//BaseUrl/api/NameControler
        [Cached(100)]
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProduct([FromQuery] ProductSpecParams specParams)//End Point
        {
            var Result = await productService.GetAllProductAsync(specParams);
            return Ok(Result);
        }
        [ProducesResponseType(typeof(IEnumerable<BrandTypeDto>), StatusCodes.Status200OK)]

        [HttpGet("AllBrand")]//BaseUrl/api/NameControler
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetAllBrands()//End Point
        {
            var Result = await productService.GetAllBrandAsync();
            return Ok(Result);
        }

        [ProducesResponseType(typeof(IEnumerable<BrandTypeDto>), StatusCodes.Status200OK)]

        [HttpGet("AllType")]//BaseUrl/api/NameControler
        public async Task<ActionResult<IEnumerable<BrandTypeDto>>> GetAllTypes()//End Point
        {
            var Result = await productService.GetAllTypeAsync();
            return Ok(Result);
        }


        [ProducesResponseType(typeof(BrandTypeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(APIErrorResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]//BaseUrl/api/NameControler
        public async Task<IActionResult> GetProductById(int? id)//End Point
        {
            if (id == null)
            {
                return BadRequest(new APIErrorResponse(400));
            }
            var Result = await productService.GetProductByIdAsync(id.Value);

            if (Result is null)
            {
                return NotFound(new APIErrorResponse(404));
            }
            return Ok(Result);
        }
    }
}
