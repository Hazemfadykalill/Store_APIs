using AutoMapper;
using Store.HazemFady.Core.Dtos.Baskets;
using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Mapping.Baskets
{
    public  class BasketProfile:Profile
    {

        public BasketProfile()
        {
                CreateMap<UserBasket,BasketDTO>().ReverseMap();
        }
    }
}
