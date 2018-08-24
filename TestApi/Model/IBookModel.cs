using System;
using System.Collections.Generic;
using TestApi.ORMapper.Models;

namespace TestApi.Model
{
  public interface IBookModel : IDisposable
    {
      IEnumerable<Book> Books { get; }
      IEnumerable<Book> GetBooksThatMatchesSearchText(string searchText);
      IEnumerable<Book> Add(Book newBook);
      IEnumerable<Book> Delete(string id);
  }
}
