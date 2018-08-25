using System.Collections.Generic;

namespace TestApi.Model
{
  public interface IRepository
    {
        TEntity GetById<TEntity>(int id) where TEntity : class;
        TEntity GetById<TEntity>(string id) where TEntity : class;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class;

        void Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

  }
}
