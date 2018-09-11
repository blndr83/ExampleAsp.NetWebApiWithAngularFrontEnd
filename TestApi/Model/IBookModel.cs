using System.Collections.Generic;
using TestApi.ORMapper.Models;

namespace TestApi.Model
{
  public interface IBookModel
    {
      IEnumerable<Book> Books { get; }
      void Add(Book newBook);
      void Delete(string id);
      void Update(Book bookToUpdate);
  }
}
