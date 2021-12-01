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
    public class GenreServiceTests: DbTests
    {
        private GenreService _service;

        public GenreServiceTests() : base(new DbContextOptionsBuilder<PlaybillDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=Playbill_TestDb;Username=postgres;Password=qwerty;")
            .Options)
        {

        }

        [SetUp]
        public void SetUp()
        {
            var context = new PlaybillDbContext(ContextOptions);
            var unit = new UnitOfWork(context);
            _service = new GenreService(new GenreBackMapper(), unit, new GenreMapper());
            const string name = "CreatedName";
            var entity = new Genre()
            {
                Name = name
            };
            unit.Genres.Create(entity).Wait();
        }
        

        [Test]
        [TestCase("NewName")]
        public void Create_AddingGenre_CheckingCreation(string name)
        {
            var genre = new GenreModel
            {
                Name = name,
            };

            _service.Create(genre).Wait();
            using var context = new PlaybillDbContext(ContextOptions);
            var entity = context.Genres.Where(x => name == x.Name).FirstOrDefaultAsync().Result;

            Assert.NotNull(entity);
        }

        [Test]
        [TestCase("CreatedName")]
        public void Create_AddingExistingGenre_GetCreationException(string name)
        {
            var genre = new GenreModel
            {
                Name = name,
            };

            Assert.ThrowsAsync<CreationException>(() => _service.Create(genre));
        }
        [Test]
        [TestCase("CreatedName")]
        public void GetId_CreatedGenre_GetCorrectId(string name)
        {
            var genre = new GenreModel
            {
                Name = name
            };
            var id =  _service.GetId(genre).Result;

            using var context = new PlaybillDbContext(ContextOptions);
            var entity = context.Genres.Where(x => x.Name == name)
                .FirstOrDefaultAsync().Result;

            Assert.AreEqual(entity.Id, id);
        }

        [Test]
        [TestCase("NotCreatedName", "NotCreatedLastName")]
        public void GetId_NotCreatedGenre_GetNotFoundException(string name, string lastName)
        {
            var genre = new GenreModel
            {
                Name = name
            };
            Assert.ThrowsAsync<NotFoundException>(()=> _service.GetId(genre));
        }
    }
}