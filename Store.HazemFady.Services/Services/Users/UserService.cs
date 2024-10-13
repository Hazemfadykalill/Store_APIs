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
        public async Task<UserResponseAfterLoginDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            if (await CheckEmailExitsAsync(registerDTO.Email)) return null!;
            var appUser = new APPUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                DisplayName = registerDTO.DisplayName,
                UserName = registerDTO.Email.Split("@")[0]
            };
            var Result = await userManager.CreateAsync(appUser, registerDTO.Password);
            if (!Result.Succeeded) return null!;
            return new UserResponseAfterLoginDTO()
            {

                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                Token = await tokenService.CreateTokenAsync(appUser, userManager)
            };
        }

        public async Task<bool> CheckEmailExitsAsync(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
