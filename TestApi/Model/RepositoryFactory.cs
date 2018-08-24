using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace TestApi.Model
{
  public class RepositoryFactory
    {
        public static T GetRepository<T>(DbContext context)
        {
            var type = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(T))).FirstOrDefault();
            return (T)Activator.CreateInstance(type, context);
        }
    }
}
