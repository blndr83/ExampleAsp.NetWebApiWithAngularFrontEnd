using Microsoft.EntityFrameworkCore;

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
