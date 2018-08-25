using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace TestApi.Model
{
  public class RepositoryFactory
    {
        public static IRepository GetRepository(DbContext context)
        {
            return new Repository(context);
        }
    }
}
