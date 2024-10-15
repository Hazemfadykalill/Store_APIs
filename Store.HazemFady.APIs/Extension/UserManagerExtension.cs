using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.HazemFady.APIs.Errors;
using Store.HazemFady.Core.Entities.Identity;
using System.Security.Claims;

namespace Store.HazemFady.APIs.Extension
{
    public static class UserManagerExtension
    {

        public static async  Task<APPUser> FindByEmailWithAddressAsync(this UserManager<APPUser> userManager,ClaimsPrincipal User)
        {

            var UserEmail = User.FindFirstValue(ClaimTypes.Email);
    
            if (UserEmail == null) return null!;
           var user=await userManager.Users.Include(x => x.Address).FirstOrDefaultAsync(x=>x.Email==UserEmail);
            if (user == null) return null!;
            return user;


        }
    }
}
