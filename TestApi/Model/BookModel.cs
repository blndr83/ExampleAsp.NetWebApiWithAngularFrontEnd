using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestApi.ORMapper.Models;

namespace TestApi.Model
{
  public class BookModel : ModelBase, IBookModel
  {

      public BookModel(DbContext context) : base(context)
      {

      }

      public IEnumerable<Book> Books => GetAll<Book>();


      public void Add(Book newBook)
      {
        if(!string.IsNullOrWhiteSpace(newBook.ArticleNumber) && !string.IsNullOrWhiteSpace(newBook.Name))
        {
            var books = GetAll<Book>();
            if (!books.Any(b => b.ArticleNumber.ToLower().Equals(newBook.ArticleNumber.ToLower())))
            {
              Add<Book>(newBook);
            }
        }

      }

      public void Delete(string id)
      {
        var book = GetById<Book>(id);
        Remove(book);
      }

      public IEnumerable<Book> GetBooksThatMatchesSearchText(string searchText)
      {
        return GetAll<Book>().Where(b => b.ArticleNumber.ToLower().Contains(searchText.ToLower()) || b.Name.ToLower().Contains(searchText.ToLower()));
      }
  }
}
