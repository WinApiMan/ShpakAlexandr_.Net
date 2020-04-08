using System;
using System.Collections.Generic;
using System.Linq;

namespace Taxi.DAL.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        void Create(TEntity item);

        void AddRange(IEnumerable<TEntity> list);

        TEntity FindById(int id);

        IQueryable<TEntity> Get();

        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);

        void Remove(TEntity item);

        void Update(TEntity item);
    }
}