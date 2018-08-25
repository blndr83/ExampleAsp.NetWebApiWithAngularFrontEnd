using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TestApi.Model
{
  public class Repository : IRepository
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
    {
           _context.Set<TEntity>().Add(entity);
           _context.SaveChanges();
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public TEntity GetById<TEntity>(int id) where TEntity : class
        {
            return _context.Set<TEntity>().Find(id);
        }

        public TEntity GetById<TEntity>(string id) where TEntity : class
    {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class
    {
            return _context.Set<TEntity>().ToList();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
    {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
            _context.Set<TEntity>().RemoveRange(entities);
            _context.SaveChanges();
        }
    }
}
