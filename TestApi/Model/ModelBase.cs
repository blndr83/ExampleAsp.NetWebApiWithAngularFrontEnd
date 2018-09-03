using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestApi.ORMapper;

namespace TestApi.Model
{
  public abstract class ModelBase<TEntity> : IDisposable where TEntity : Entity  
  {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected ModelBase(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected void Insert(TEntity entity)
        {
            try
            {
              _dbSet.Add(entity);
              _context.SaveChanges();
            }
            catch (Exception) { }

        }

        protected void Update(TEntity entity, TEntity oldEntity)
        {
            _context.Entry(oldEntity).State = EntityState.Detached;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        protected void AddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.AddRange(entities);
                _context.SaveChanges();
            }
            catch (Exception){ }
        }

        protected IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            return _dbSet.Where(filter).ToList();
        }

        protected TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        protected TEntity GetById(string id)
        {
            return _dbSet.Find(id);
        }

        protected IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        protected void Remove(TEntity entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception){ }
        }

        protected void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
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
