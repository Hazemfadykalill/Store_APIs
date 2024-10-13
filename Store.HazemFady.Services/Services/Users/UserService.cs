using Microsoft.AspNetCore.Identity;
using Store.HazemFady.Core.Dtos.Auth;
using Store.HazemFady.Core.Entities.Identity;
using Store.HazemFady.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Services.Services.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<APPUser> userManager;
        private readonly SignInManager<APPUser> signInManger;
        private readonly ITokenService tokenService;

        public UserService(UserManager<APPUser> userManager, SignInManager<APPUser> signInManger, ITokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManger = signInManger;
            this.tokenService = tokenService;
        }
        //this method not end point
        public async Task<UserResponseAfterLoginDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return null!;
            var Result = await signInManger.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!Result.Succeeded) return null!;
            return new UserResponseAfterLoginDTO()
            {
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await tokenService.CreateTokenAsync(user, userManager)

            };



        }
        //this method not end point
        public Task<UserResponseAfterLoginDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            throw new NotImplementedException();
        }
    }
}
