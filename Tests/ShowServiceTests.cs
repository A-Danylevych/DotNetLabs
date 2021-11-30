using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Impl.Mappers;
using BLL.Impl.Services;
using DAL.Entities;
using DAL.Impl.Postgres;
using Microsoft.EntityFrameworkCore;
using Models.Base;
using Models.Error;
using NUnit.Framework;

namespace Tests
{
    public class ShowServiceTests : DbTests
    {
        private ShowService _service;
        private UnitOfWork _unit;

        public ShowServiceTests() : base(new DbContextOptionsBuilder<PlaybillDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=Playbill_TestDb;Username=postgres;Password=qwerty;")
            .Options)
        {

        }

        [SetUp]
        public void SetUp()
        {
            var context = new PlaybillDbContext(ContextOptions);
            _unit = new UnitOfWork(context);
            _service = new ShowService(new ShowBackMapper(), new ShowMapper(), _unit,
                new AuthorService(new AuthorBackMapper(), _unit),
                new GenreService(new GenreBackMapper(), _unit));
            const string name = "Name";
            var entityGenre = new Genre()
            {
                Name = name
            };
            _unit.Genres.Create(entityGenre).Wait();
            const string lastName = "CreatedLastName";
            var entityAuthor = new Author
            {
                FirstName = name,
                LastName = lastName,
            };
            _unit.Authors.Create(entityAuthor).Wait();
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10, 
                TimeSpan.Zero);
            var entityShow = new Show
            {
                AuthorId = _unit.Authors.FindAuthor(name,lastName).Result.Id,
                Date = date,
                Genres = new List<Genre>() {entityGenre},
                Name = name,
            };
            _unit.Shows.Create(entityShow).Wait();
        }
        [Test]
        [TestCase("NewName")]
        public void Create_AddingShow_CheckingCreation(string name)
        {
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10,
                TimeSpan.Zero);

            const string test = "ForTest";
            var authorEntity = new Author
            {
                FirstName = test,
                LastName = test
            };
            var genreEntity = new Genre
            {
                Name = test,
            };

            _unit.Authors.Create(authorEntity).Wait();
            _unit.Genres.Create(genreEntity).Wait();

            var author = _unit.Authors.FindAuthor(test, test).Result;
            var genre =  _unit.Genres.FindGenre(test).Result;

            var show = new ShowModel
            {
                Name = name,
                AuthorId = author.Id,
                Date = date,
                GenresIds = new List<int>{genre.Id},
            };
            
            _service.Create(show).Wait();
            
            using var context = new PlaybillDbContext(ContextOptions);
            var entity = context.Shows.Where(x => x.Name == name && x.AuthorId == author.Id
                                                                 && x.Date == date)
                .FirstOrDefaultAsync().Result;
            
            Assert.NotNull(entity);
        }

        [Test]
        [TestCase("Name")]
        public void Create_AddingExistingShow_NothingChanged(string showName)
        {
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10,
                TimeSpan.Zero);
            const int expected = 1;
            const string name = "Name";
            const string lastName = "CreatedLastName";
            var author = _unit.Authors.FindAuthor(name, lastName).Result;
            var genre = _unit.Genres.FindGenre(name).Result;
           
            var show = new ShowModel
            {
                Name = name,
                AuthorId = author.Id,
                Date = date,
                GenresIds = new List<int>{genre.Id},
            };
            
            Assert.AreEqual(expected, _unit.Shows.GetAll().Result.Count);
            _service.Create(show).Wait();
            
            Assert.AreEqual(expected, _unit.Shows.GetAll().Result.Count);
        }

        [Test]
        public void FindByAuthor_AuthorAlreadyCreating_GetCollectionCountOne()
        {
            const string name = "Name";
            const string lastName = "CreatedLastName";
            const int count = 1;
            var author = new AuthorModel
            {
                FirstName = name,
                LastName = lastName,
            };
            
            var list = _service.FindByAuthor(author).Result;
            
            Assert.AreEqual(count, list.Count);
        }
        [Test]
        public void FindByGenre_GenreAlreadyCreating_GetCollectionCountOne()
        {
            const string name = "Name";
            const int count = 1;
            var genre = new GenreModel()
            {
                Name = name,
            };
            
            var list =  _service.FindByGenres(new List<GenreModel>{genre}).Result;
            
            Assert.AreEqual(count, list.Count);
        }
        [Test]
        public void FindByDate_DateAlreadyCreating_GetCollectionCountOne()
        {
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10,
                TimeSpan.Zero);
            const int count = 1;


            var list =  _service.FindByDate(date).Result;
            
            Assert.AreEqual(count, list.Count);
        }
    }
}