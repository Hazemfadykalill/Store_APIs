using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.Core.Dtos.Auth;
using Store.HazemFady.Core.Services.Contract;

namespace Store.HazemFady.APIs.Controllers
{

    public class AccountsController : BaseAPIController
    {
        private readonly IUserService userService;

        public AccountsController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseAfterLoginDTO>>Login(LoginDTO loginDTO)
        {
            var UserDto=await userService.LoginAsync(loginDTO);
            if(UserDto is null) return  Unauthorized(new APIErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(UserDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseAfterLoginDTO>> Register(RegisterDTO registerDTO)
        {
            var UserDto = await userService.RegisterAsync(registerDTO);
            if (UserDto is null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest,"Invalid registration !! "));
            return Ok(UserDto);
        }
    }
}
