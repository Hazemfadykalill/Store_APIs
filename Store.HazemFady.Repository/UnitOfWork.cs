using Store.HazemFady.Core;
using Store.HazemFady.Core.Entities;
using Store.HazemFady.Core.Repositories.Contract;
using Store.HazemFady.Repository.Data.Contexts;
using Store.HazemFady.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.HazemFady.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext storeDb;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext storeDb)
        {
            this.storeDb = storeDb;
            _repositories = new Hashtable();
        }
        public async  Task<int> CompleteAsync()
        {
            return await storeDb.SaveChangesAsync();
        }

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository=new GenericRepository<TEntity, TKey>(storeDb);
                _repositories.Add(type, repository);    
            }

            return  _repositories[type] as IGenericRepository<TEntity, TKey>;
        }
    }
}
