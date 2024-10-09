using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.APIs.Errors;

namespace Store.HazemFady.APIs.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorsController : ControllerBase
    {

        //Have End Point 
        [HttpGet]
        public IActionResult Error(int code)
        {
            return NotFound(new APIErrorResponse(StatusCodes.Status404NotFound,"Not Found End Point"));
        }
    }
}
