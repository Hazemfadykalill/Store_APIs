﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.Core.Services.Contract;

namespace Store.HazemFady.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("AllProduct")]//BaseUrl/api/NameControler

        
        public async  Task<IActionResult> GetAllProduct([FromQuery] string? sort, [FromQuery] int? PageSize=5, [FromQuery] int? PageIndex=1)//End Point
        {
           var Result = await productService.GetAllProductAsync(sort,PageSize,PageIndex);
            return Ok(Result);
        }
        [HttpGet("AllBrand")]//BaseUrl/api/NameControler
        public async Task<IActionResult> GetAllBrands()//End Point
        {
            var Result = await productService.GetAllBrandAsync();
            return Ok(Result);
        }
        [HttpGet("AllType")]//BaseUrl/api/NameControler
        public async Task<IActionResult> GetAllTypes()//End Point
        {
            var Result = await productService.GetAllTypeAsync();
            return Ok(Result);
        }

        [HttpGet("{id}")]//BaseUrl/api/NameControler
        public async Task<IActionResult> GetProductById(int? id)//End Point
        {
            if(id == null)
            {
                return BadRequest("Invalid Id!!");
            }
            var Result = await productService.GetProductByIdAsync(id.Value);

            if(Result is null)
            {
                return NotFound($"The Product with Id {id} Not found Database");
            }
            return Ok(Result);
        }
    }
}
