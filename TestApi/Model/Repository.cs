using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TestApi.ORMapper;

namespace TestApi.Model
{
  public class Repository : IRepository
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : Entity
        {
            try
            {
              _context.Set<TEntity>().Add(entity);
              _context.SaveChanges();
            }
            catch (Exception) { }

        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
        {
            try
            {
                _context.Set<TEntity>().AddRange(entities);
                _context.SaveChanges();
            }
            catch (Exception){ }
        }

        public TEntity GetById<TEntity>(int id) where TEntity : Entity
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity GetById<TEntity>(string id) where TEntity : Entity
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : Entity
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : Entity
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception){ }
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : Entity
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
