using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using Store.HazemFady.Core.Entities.Identity;
using Store.HazemFady.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Services.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        //fun to create token type [JWT]
        public async  Task<string> CreateTokenAsync(APPUser APPUser,UserManager<APPUser>userManager)
        {
            //1.Header

            //2.PayLoad
            //3.Signature

            var UserRoles=await userManager.GetRolesAsync(APPUser);
            var authClaims=new List<Claim>()
            {
                new Claim(ClaimTypes.Email,APPUser.Email!),
                new Claim(ClaimTypes.GivenName,APPUser.DisplayName),
                new Claim(ClaimTypes.MobilePhone,APPUser.PhoneNumber!),
                new Claim(ClaimTypes.MobilePhone,APPUser.PhoneNumber!),
            };

            foreach (var Role in UserRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, Role));


            }

            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var token = new JwtSecurityToken
                (
                issuer: configuration["JWT:Issure"],
                audience: configuration["JWT:Audience"],
                expires:DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDay"])),
                claims:authClaims,
                signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.HmacSha256)
                //signingCredentials:new SigningCredentials(AuthKey,SecurityAlgorithms.EcdsaSha256Signature)

                );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
