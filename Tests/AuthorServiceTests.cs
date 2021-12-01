using System.Linq;
using BLL.Abstracts.IService;
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
    public class AuthorServiceTests: DbTests
    {
        public AuthorServiceTests() : base(new DbContextOptionsBuilder<PlaybillDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=Playbill_TestDb;Username=postgres;Password=qwerty;")
            .Options)
        {
            
        }
        
        [SetUp]
        public void SetUp()
        {
            var context = new PlaybillDbContext(ContextOptions);
            var unit = new UnitOfWork(context);
            _service = new AuthorService(new AuthorBackMapper(), unit, new AuthorMapper());
            const string name = "CreatedName";
            const string lastName = "CreatedLastName";
            var entity = new Author
            {
                FirstName = name,
                LastName = lastName,
            };
            unit.Authors.Create(entity).Wait();
        }

        private IAuthorService _service;
        
        [Test]
        [TestCase("Name", "LastName")]
        public void Create_AddingAuthor_CheckingCreation(string name, string lastName)
        {
            var author = new AuthorModel
            {
                FirstName = name,
                LastName = lastName,
            };
            
            _service.Create(author).Wait();

            using var context = new PlaybillDbContext(ContextOptions);
            var entity = context.Authors.Where(x => x.FirstName == name && x.LastName == lastName)
                .FirstOrDefaultAsync().Result;
            
            Assert.NotNull(entity);
        }

        [Test]
        [TestCase("CreatedName", "CreatedLastName")]
        public void Create_AddingExistingAuthor_GetCreationException(string name, string lastName)
        {
            var author = new AuthorModel
            {
                FirstName = name,
                LastName = lastName,
            };

            Assert.ThrowsAsync<CreationException>(() => _service.Create(author));
        }

        [Test]
        [TestCase("CreatedName", "CreatedLastName")]
        public void GetId_CreatedAuthor_GetCorrectId(string name, string lastName)
        {
            var author = new AuthorModel
            {
                FirstName = name,
                LastName = lastName,
            };
            var id =  _service.GetId(author).Result;

            using var context = new PlaybillDbContext(ContextOptions);
            var entity = context.Authors.Where(x => x.FirstName == name && x.LastName == lastName)
                .FirstOrDefaultAsync().Result;

            Assert.AreEqual(entity.Id, id);
        }

        [Test]
        [TestCase("NotCreatedName", "NotCreatedLastName")]
        public void GetId_NotCreatedAuthor_GetNotFoundException(string name, string lastName)
        {
            var author = new AuthorModel
            {
                FirstName = name,
                LastName = lastName,
            };
            Assert.ThrowsAsync<NotFoundException>(()=> _service.GetId(author));
        }
    }
}