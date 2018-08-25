using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestApi.ORMapper.Models;

namespace TestApi.Model
{
  public class BookModel : IBookModel
    {
      private readonly DbContext _context;

      public BookModel(TestDatenbankContext context)
      {
          _context = context;
          Repository = RepositoryFactory.GetRepository(_context);
      }

    public IEnumerable<Book> Books => Repository.GetAll<Book>();

    private IRepository Repository { get; }

    public IEnumerable<Book> Add(Book newBook)
    {
      var books = Repository.GetAll<Book>();
      if (!books.Any(b => b.ArticleNumber.ToLower().Equals(newBook.ArticleNumber.ToLower())))
      {
        Repository.Add(newBook);
      }
      return Books;
    }

    public IEnumerable<Book> Delete(string id)
    {
      var book = Repository.GetById<Book>(id);
      Repository.Remove(book);
      return Books;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IEnumerable<Book> GetBooksThatMatchesSearchText(string searchText)
    {
      return Repository.GetAll<Book>().Where(b => b.ArticleNumber.ToLower().Contains(searchText.ToLower()) || b.Name.ToLower().Contains(searchText.ToLower()));
    }
  }
}
