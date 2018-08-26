using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
            var book = new Book() { ArticleNumber = articleNumber, Name = name };
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
        [InlineData("A6362")]
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

        [Theory]
        [InlineData("A", 2)]
        [InlineData("oo", 2)]
        [InlineData("l", 2)]
        [InlineData(" ", 3)]
        [InlineData("D", 3)]
        [InlineData("m", 1)]
        [InlineData("die", 1)]
        [InlineData("23", 1)]
        public void TestGetBooksThatMatchesSearchText(string searchText, int amountOfExpectedItems)
        {
            AddBooksForTestGetBooksThatMatchesSearchText();
            var books = _model.GetBooksThatMatchesSearchText(searchText);
            Assert.True(books.Count() == amountOfExpectedItems);
            Assert.True(books.All(b => b.ArticleNumber.ToLower().Contains(searchText.ToLower())
            || b.Name.ToLower().Contains(searchText.ToLower())));
        }

        private void AddBooksForTestGetBooksThatMatchesSearchText()
        {
            var books = new List<Book>()
            {
                new Book() { ArticleNumber = "A98", Name = "Book One" },
                new Book() { ArticleNumber = "BC13", Name = "Too Hard" },
                new Book() { ArticleNumber = "9H5L", Name = "Rincewind" },
                new Book() { ArticleNumber = "Gkl023", Name = "Los" },
                new Book() { ArticleNumber = "NBC092", Name = "Der Dieb" },
                new Book() { ArticleNumber = "521J", Name = "Mehrwert" }
            };

            books.ForEach(b => _model.Add(b));
        }
    }
}
