using Microsoft.AspNetCore.Identity;
using Store.HazemFady.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Services.Contract
{
    public  interface ITokenService
    {

       Task<string> CreateTokenAsync(APPUser APPUser,UserManager<APPUser> userManager);
    }
}
