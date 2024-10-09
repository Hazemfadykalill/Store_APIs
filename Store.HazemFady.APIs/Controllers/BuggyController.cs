using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.Repository.Data.Contexts;

namespace Store.HazemFady.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreDbContext storeDb;

        public BuggyController(StoreDbContext storeDb)
        {
            this.storeDb = storeDb;
        }
        [HttpGet("NotFound")]
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brands=await storeDb.Brands.FindAsync(100);
            if (brands == null) 
                return NotFound(new {Message="Brand With Id :100 is not Found",StatusCode=StatusCodes.Status404NotFound});

            return Ok(brands);
        }

        [HttpGet("ServerError")]
        public async Task<IActionResult> GetServerRequestError()
        {
            var brands = await storeDb.Brands.FindAsync(100);
            var BrandToString=brands!.ToString();//will throw Null Exception

            return Ok(brands);
        }

        [HttpGet("BadRequest")]
        public IActionResult  GetBadRequestError()
        {
      

            return BadRequest();
        }

        [HttpGet("BadRequest/{id}")]
        public IActionResult GetBadRequestError(int id)//Validation Error
        {


            return Ok();
        }


        [HttpGet("Unauthorized")]
        public IActionResult GetUnauthorizedError(int id)
        {


            return Unauthorized();
        }


    }
}
