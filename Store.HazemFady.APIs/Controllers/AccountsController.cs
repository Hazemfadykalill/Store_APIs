using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.APIs.Extension;
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
        private readonly IMapper mapper;

        public AccountsController(IUserService userService, UserManager<APPUser> userManager, ITokenService tokenService,IMapper mapper)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
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
        [Authorize]
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

        [Authorize]
        //Get Address To Current User
        [HttpGet("Address")]
        public async Task<ActionResult<UserResponseAfterLoginDTO>> GetAddressToCurrentUser()
        {

            //Note this FindByEmailAsync Not Load Navigational Property .
            var user = await userManager.FindByEmailWithAddressAsync(User);
            if (user == null) return BadRequest(new APIErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(mapper.Map<AddressDTO>(user.Address));
        }
    }
}
