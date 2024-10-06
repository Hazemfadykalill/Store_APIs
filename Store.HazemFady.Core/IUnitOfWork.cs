using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Core
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        //Create Repository And Return it
        IGenericRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity:BaseEntity<TKey>;
     }
}
