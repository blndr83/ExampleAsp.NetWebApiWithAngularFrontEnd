using Microsoft.EntityFrameworkCore;
using TestApi.ORMapper.Models;

namespace TestApi.Model
{
  public class BookRepository : Repository<Book>, IBookRepository
    {

        public BookRepository(DbContext context)
            : base(context)
        {

        }


    }
}
