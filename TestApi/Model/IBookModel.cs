using System.Collections.Generic;
using TestApi.ORMapper.Models;

namespace TestApi.Model
{
  public interface IBookModel
    {
      IEnumerable<Book> Books { get; }
      IEnumerable<Book> GetBooksThatMatchesSearchText(string searchText);
      void Add(Book newBook);
      void Delete(string id);
  }
}
