using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Specifications.Products
{
    public  class ProductSpecification:BaseSpecification<Product,int>
    {

        public ProductSpecification()
        {

            Criteria = null;
            //Includes.Add(P => P.Brand);
            //Includes.Add(P => P.Type);

            ApplyIncludes();

        }

        public ProductSpecification(int id)
        {

            Criteria = P=>P.Id==id;
            //Includes.Add(P => P.Brand);
            //Includes.Add(P => P.Type);

            ApplyIncludes();
        }

        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);

        }
    }
}
