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
            // the serviceProvider make it possible that each test has his own Database
            // for more information watch https://www.youtube.com/watch?v=BL5Mdx1sUpQ
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
            var options = new DbContextOptionsBuilder<TestDatenbankContext>().UseInMemoryDatabase()
                .UseInternalServiceProvider(serviceProvider)
                .Options;
            var context = new TestDatenbankContext(options);
            _model = new BookModel(context);
        }

        [Theory]
        [InlineData("A123", "Test Book")]
        [InlineData("  A123  ", " Test Book")]
        public void TestAdd(string articleNumber, string name)
        {
            var book = new Book() { ArticleNumber = articleNumber, Name = name};
            _model.Add(book);
            var books = _model.Books;
            Assert.True(books.Count() == 1);
            Assert.Contains(books, b => b.ArticleNumber.Equals(articleNumber.Trim()) && b.Name.Equals(name.Trim()));
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

        [Theory]
        [InlineData("A70", "   ")]
        [InlineData("", " Rincewind")]
        [InlineData("  ", "   ")]
        public void TestInvalidBookWillnotBeAdded(string articleNumber, string name)
        {
            var book = new Book() { ArticleNumber = articleNumber, Name = name };
            _model.Add(book);
            var books = _model.Books;
            Assert.False(books.Any());
        }

        [Theory]
        [InlineData("     ")]
        [InlineData(" ABC")]
        public void TestDeleteWithInvalidId(string id)
        {
            var book = new Book() { ArticleNumber = "ABC", Name = "Test Book 3" };
            _model.Add(book);
            var books = _model.Books;
            Assert.True(books.Count() == 1);
            _model.Delete(id);
            books = _model.Books;
            Assert.True(books.Count() == 1);
        }

        [Fact]
        public void TestDeleteWithNotExistingId()
        {
            var book = new Book() { ArticleNumber = "ABCY", Name = "Test Book 5" };
            _model.Add(book);
            var books = _model.Books;
            Assert.True(books.Count() == 1);
            _model.Delete("A6362");
            books = _model.Books;
            Assert.True(books.Count() == 1);
        }

        [Theory]
        [InlineData("ABCGHY", "Nice Book")]
        [InlineData("A65", "Test Book 89")]
        [InlineData("ABCGHY", "Test Book 89")]
        [InlineData(" ABCGHY ", "Nice Book")]
        [InlineData("A65", "Test Book 89 ")]
        [InlineData(" ABCGHY", " Test Book 89")]
        public void TestBookWithDuplicatedIdAndNameWillNotBeAdded(string articleNumber, string name)
        {
            var book = new Book() { ArticleNumber = "ABCGHY", Name = "Test Book 89" };
            _model.Add(book);
            var books = _model.Books;
            Assert.True(books.Count() == 1);
            var newBook = new Book() { ArticleNumber = articleNumber, Name = name };
            _model.Add(newBook);
            books = _model.Books;
            Assert.True(books.Count() == 1);
        }
    }
}
