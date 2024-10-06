 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Repository
{
   public  class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        //This Func To Create And Return Query
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity,TKey> Spec)
        {
            var Query = inputQuery;


            //1.Check if query that contain filter or no
            if(Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria);
            }

            if ( Spec.OrderBy is not null)
            {

                Query = Query.OrderBy(Spec.OrderBy);  

            }

            if (Spec.OrderByDescending is not null)
            {

                Query = Query.OrderBy(Spec.OrderByDescending);

            }

            if (Spec.IsPaginationEnabled)
            {
                Query =  Query.Skip(Spec.Skip).Take(Spec.Take);
            }
            Query =Spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));


            return Query;

        }
        //  storeDb.Products.Include(B => B.Brand).Include(T => T.Type).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;

        //storeDb.Products ===>input Any Query

        //Include(B => B.Brand).Include(T => T.Type).FirstOrDefaultAsync(P => P.Id == id as int?)==>Details Any Query
    }
}
