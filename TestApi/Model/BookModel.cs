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
        if(!string.IsNullOrWhiteSpace(newBook.ArticleNumber) && !string.IsNullOrWhiteSpace(newBook.Title))
        {
            var books = GetAll<Book>();
            newBook.ArticleNumber = newBook.ArticleNumber.Trim();
            newBook.Title = newBook.Title.Trim();
            if (!books.Any(b => b.ArticleNumber.ToLower().Equals(newBook.ArticleNumber.ToLower())
              || b.Title.ToLower().Equals(newBook.Title.ToLower())))
            {
              Add<Book>(newBook);
            }
        }

      }

      public void Delete(string id)
      {
        if(!string.IsNullOrWhiteSpace(id))
        {
           var book = GetById<Book>(id);
           if(book != null)  Remove(book);
        }

      }

      public IEnumerable<Book> GetBooksThatMatchesSearchText(string searchText)
      {
        return GetAll<Book>().Where(b => b.ArticleNumber.ToLower().Contains(searchText.ToLower()) || b.Title.ToLower().Contains(searchText.ToLower()));
      }

      public void Update(Book bookToUpdate)
      {
        var book = GetById<Book>(bookToUpdate.ArticleNumber);
        if (book != null)
        {
          book.IsLoaned = bookToUpdate.IsLoaned;
          book.Title = book.Title;
          Update<Book>(book);
        }
    }
  }
}
