using System;
using System.Collections.Generic;
using TestApi.ORMapper;

namespace TestApi.Model
{
  public interface IRepository : IDisposable
  {
        TEntity GetById<TEntity>(int id) where TEntity : Entity;
        TEntity GetById<TEntity>(string id) where TEntity : Entity;
        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : Entity;

        void Add<TEntity>(TEntity entity) where TEntity : Entity;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;

        void Remove<TEntity>(TEntity entity) where TEntity : Entity;
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity;

  }
}
