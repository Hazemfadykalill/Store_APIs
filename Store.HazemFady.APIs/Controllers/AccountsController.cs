using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.Core.Dtos.Auth;
using Store.HazemFady.Core.Entities.Identity;
using Store.HazemFady.Core.Services.Contract;
using System.Security.Claims;

namespace Store.HazemFady.APIs.Controllers
{

    public class AccountsController : BaseAPIController
    {
        private readonly IUserService userService;
        private readonly UserManager<APPUser> userManager;
        private readonly ITokenService tokenService;

        public AccountsController(IUserService userService, UserManager<APPUser> userManager, ITokenService tokenService)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.tokenService = tokenService;
        }
        //To Login User
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseAfterLoginDTO>> Login(LoginDTO loginDTO)
        {
            var UserDto = await userService.LoginAsync(loginDTO);
            if (UserDto is null) return Unauthorized(new APIErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(UserDto);
        }
        //To Register New Account
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseAfterLoginDTO>> Register(RegisterDTO registerDTO)
        {
            var UserDto = await userService.RegisterAsync(registerDTO);
            if (UserDto is null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest, "Invalid registration !! "));
            return Ok(UserDto);
        }
        //Get Current User
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserResponseAfterLoginDTO>> GetCurrentUser()
        {
            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
            var user = await userManager.FindByEmailAsync(UserEmail!);
            if (user == null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(new UserResponseAfterLoginDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }
    }
}
