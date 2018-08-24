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
            BookRepository = RepositoryFactory.GetRepository<IBookRepository>(_context);
        }

    public IEnumerable<Book> Books => BookRepository.GetAll();

    private IBookRepository BookRepository { get; }

    public IEnumerable<Book> Add(Book newBook)
    {
      var books = BookRepository.GetAll();
      if (!books.Any(b => b.ArticleNumber.ToLower().Equals(newBook.ArticleNumber.ToLower())))
      {
        BookRepository.Add(newBook);
      }
      return Books;
    }

    public IEnumerable<Book> Delete(string id)
    {
      var book = BookRepository.GetById(id);
      BookRepository.Remove(book);
      return Books;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public IEnumerable<Book> GetBooksThatMatchesSearchText(string searchText)
    {
      return BookRepository.GetAll().Where(b => b.ArticleNumber.ToLower().Contains(searchText.ToLower()) || b.Name.ToLower().Contains(searchText.ToLower()));
    }
  }
}
