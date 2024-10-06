using Microsoft.EntityFrameworkCore;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Repositories.Contract;
using Store.HazemFady.Core.Specifications;
using Store.HazemFady.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext storeDb;

        public GenericRepository(StoreDbContext storeDb)
        {
            this.storeDb = storeDb;
        }


        //Without Spec
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>)await storeDb.Products.Include(B => B.Brand).Include(T => T.Type).ToListAsync();

            }
            return await storeDb.Set<TEntity>().ToListAsync();
        }


        //With Spec
        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey> Spec)
        {
            return await ApplySpecification(Spec).ToListAsync();
        }



        public async Task<TEntity> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await storeDb.Products.Include(B => B.Brand).Include(T => T.Type).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;

            }
            return await storeDb.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetWithSpecAsync(ISpecification<TEntity, TKey> Spec)
        {
            return await ApplySpecification(Spec).FirstOrDefaultAsync();

        }
        public async Task AddAsync(TEntity entity)
        {
            await storeDb.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            storeDb.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            storeDb.Remove(entity);

        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> Spec)
        {
            return SpecificationEvaluator<TEntity, TKey>.GetQuery(storeDb.Set<TEntity>(), Spec);
        }

        public async Task<int> GetCount(ISpecification<TEntity, TKey> specification)
        {
            return await ApplySpecification(specification).CountAsync();

        }
    }
}