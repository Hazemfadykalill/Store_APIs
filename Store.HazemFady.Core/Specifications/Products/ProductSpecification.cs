using Store.HazemFady.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Specifications.Products
{
    public class ProductSpecification : BaseSpecification<Product, int>
    {

        public ProductSpecification(string? sort, int PageSize, int PageIndex)
        {

            Criteria = null;

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                       
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price); 
                        break;
                    default:

                        AddOrderBy(p => p.Name);

                        break; 


                }

            }
            else
            {
                OrderBy = p => p.Name;
            }
            //Includes.Add(P => P.Brand);
            //Includes.Add(P => P.Type);

            ApplyIncludes();

            //page size 50
            //page index 3
            ApplyPagination(PageSize*(PageIndex-1),PageSize);

        }

        public ProductSpecification(int id)
        {

            Criteria = P => P.Id == id;
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
