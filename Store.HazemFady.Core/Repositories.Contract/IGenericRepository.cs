using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core.Repositories.Contract
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        //Without Spec
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(TKey id);
        //With Spec
        Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity, TKey>Spec);
        Task<TEntity> GetWithSpecAsync(ISpecification<TEntity, TKey> Spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
