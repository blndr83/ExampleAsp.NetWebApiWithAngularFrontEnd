using System.Collections.Generic;

namespace TestApi.Model
{
  public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

    }
}
