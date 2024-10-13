using Store.HazemFady.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Services.Contract
{
    public  interface IUserService
    {

        Task<UserResponseAfterLoginDTO> LoginAsync(LoginDTO loginDTO);
        Task<UserResponseAfterLoginDTO> RegisterAsync(RegisterDTO registerDTO);

        Task<bool> CheckEmailExitsAsync(string email);
    }
}
