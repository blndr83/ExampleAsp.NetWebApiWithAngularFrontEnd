using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApi.ORMapper;

namespace TestApi.Model
{
  public abstract class ModelBase : IDisposable
  {
        protected readonly DbContext _context;

        protected ModelBase(DbContext context)
        {
            _context = context;
        }

        protected void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            try
            {
              _context.Set<TEntity>().Add(entity);
              _context.SaveChanges();
            }
            catch (Exception) { }

        }

        protected void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }
        protected void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            try
            {
                _context.Set<TEntity>().AddRange(entities);
                _context.SaveChanges();
            }
            catch (Exception){ }
        }

        protected TEntity GetById<TEntity>(int id) where TEntity : Entity
        {
            return _context.Set<TEntity>().Find(id);
        }

        protected TEntity GetById<TEntity>(string id) where TEntity : Entity
        {
            return _context.Set<TEntity>().Find(id);
        }

        protected IEnumerable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return _context.Set<TEntity>().ToList();
        }

        protected void Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception){ }
        }

        protected void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            try
            {
                _context.Set<TEntity>().RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (Exception) {}
        }

        public void Dispose()
        {
          _context.Dispose();
        }

  }
}
