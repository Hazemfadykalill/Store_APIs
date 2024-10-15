// Ignore Spelling: Auth

using AutoMapper;
using Store.HazemFady.Core.Dtos.Auth;
using Store.HazemFady.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Mapping.Auth
{
    public class AuthProfile:Profile
    {

        public AuthProfile()
        {
            CreateMap<Address,AddressDTO>().ReverseMap();
        }
    }
}
