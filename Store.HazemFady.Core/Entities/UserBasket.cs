﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Entities
{
    public class UserBasket
    {

        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}
