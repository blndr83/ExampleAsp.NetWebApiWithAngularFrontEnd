using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestApi.ORMapper.Models;

namespace TestApi.Model
{
  public class BookModel : ModelBase<Book>, IBookModel
  {

      public BookModel(DbContext context) : base(context)
      {

      }

      public IEnumerable<Book> Books => GetAll();


      public void Add(Book newBook)
      {
        if(!string.IsNullOrWhiteSpace(newBook.ArticleNumber) && !string.IsNullOrWhiteSpace(newBook.Title))
        {
            var books = GetAll();
            newBook.ArticleNumber = newBook.ArticleNumber.Trim();
            newBook.Title = newBook.Title.Trim();
            if (!books.Any(b => b.ArticleNumber.ToLower().Equals(newBook.ArticleNumber.ToLower())
              || b.Title.ToLower().Equals(newBook.Title.ToLower())))
            {
              Insert(newBook);
            }
        }

      }

      public void Delete(string id)
      {
        if(!string.IsNullOrWhiteSpace(id))
        {
           var book = GetById(id);
           if(book != null)  Remove(book);
        }

      }

      public void Update(Book bookToUpdate)
      {
        var book = GetById(bookToUpdate.ArticleNumber);
        if (book != null)
        {
          Update(bookToUpdate, book);
        }
    }
  }
}
