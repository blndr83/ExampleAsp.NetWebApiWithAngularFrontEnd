using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var options = new DbContextOptionsBuilder<TestDatenbankContext>().UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider)
                .Options;
            var context = new TestDatenbankContext(options);
            _model = new BookModel(context);
        }

        [Fact]
        public void TestAdd()
        {
            var book = new Book() { ArticleNumber = "A123", Name = "Test Book"};
            _model.Add(book);
            var books = _model.Books;
            Assert.True(books.Count() == 1);
            Assert.Contains(books, b => b.ArticleNumber.Equals(book.ArticleNumber) && b.Name.Equals(book.Name));
        }

        [Fact]
        public void TestDelete()
        {
            var book = new Book() { ArticleNumber = "A12343", Name = "Test Book 2" };
            _model.Add(book);
            var books = _model.Books;
            Assert.True(books.Count() == 1);
            _model.Delete(book.ArticleNumber);
            books = _model.Books;
            Assert.False(books.Any());
        }

        [Fact]
        public void TestInvalidBookWillnotBeAdded()
        {
            var book = new Book() { ArticleNumber = "   ", Name = "" };
            _model.Add(book);
            var books = _model.Books;
            Assert.False(books.Any());
        }
    }
}
