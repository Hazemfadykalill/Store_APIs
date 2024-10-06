using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Specifications.Products
{
    public class ProductSpecParams
    {
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
        public string Sort { get; set; } 
     

    }
}
