using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Dtos.Baskets
{
    public  class BasketDTO
    {
        public string Id { get; set; }
        public List<BasketItem> items { get; set; }

    }
}
