using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TestApi.Model;
using TestApi.ORMapper.Models;
using Xunit;

namespace XUnitTestApi
{
    public class BookModelTests : IDisposable
    {
        BookModel _model;

        public void Dispose()
        {
            _model.Dispose();
        }

        public BookModelTests()
        {
            var options = new DbContextOptionsBuilder<TestDatenbankContext>().UseInMemoryDatabase().Options;
            var context = new TestDatenbankContext(options);
            _model = new BookModel(context);
        }

        [Fact]
        public void TestAdd()
        {
            var book = new Book() { ArticleNumber = "A123", Name = "Test Book"};
            var books = _model.Add(book);
            Assert.True(books.Count() == 1);
            Assert.Contains(books, b => b.ArticleNumber.Equals(book.ArticleNumber) && b.Name.Equals(book.Name));
        }

        [Fact]
        public void TestDelete()
        {
            var book = new Book() { ArticleNumber = "A123", Name = "Test Book" };
            var books = _model.Add(book);
            Assert.True(books.Count() == 1);
            books = _model.Delete(book.ArticleNumber);
            Assert.False(books.Any());
        }
    }
}
