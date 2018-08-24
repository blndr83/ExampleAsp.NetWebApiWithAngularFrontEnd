using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TestApi.Model;
using TestApi.ORMapper.Models;

namespace TestApi.Controllers
{
  [Route("api/[controller]")]
    public class BookController : Controller
    {

        private readonly IBookModel _bookModel;

        public BookController(IBookModel bookModel)
        {
            _bookModel = bookModel;
        }

        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return  _bookModel.Books;
        }

        [HttpGet("{id}")]
        public IEnumerable<Book> Get(string id)
        {
            return _bookModel.GetBooksThatMatchesSearchText(id);
        }

        [HttpPost]
        public IEnumerable<Book> Post([FromBody] Book book)
        {
            return _bookModel.Add(book);
        }

        [HttpDelete("{id}")]
        public IEnumerable<Book> Delete(string id)
        {
            return _bookModel.Delete(id);
        }
    }
}
