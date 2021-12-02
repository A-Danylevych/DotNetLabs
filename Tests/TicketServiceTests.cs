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
using Models.Status;
using NUnit.Framework;

namespace Tests
{
    public class TicketServiceTests : DbTests
    {
        private TicketService _service;
        private UnitOfWork _unit;

        public TicketServiceTests() : base(new DbContextOptionsBuilder<PlaybillDbContext>()
            .UseNpgsql("Host=localhost;Port=5432;Database=Playbill_TestDb;Username=postgres;Password=qwerty;")
            .Options)
        {

        }

        [SetUp]
        public void SetUp()
        {
            var context = new PlaybillDbContext(ContextOptions);
            _unit = new UnitOfWork(context);
            _service = new TicketService(_unit, new TicketBackMapper(),
                new TicketUpdateMapper(), new TicketMapper());
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
                AuthorId = _unit.Authors.FindAuthor(entityAuthor.FirstName, entityAuthor.LastName).Result.Id,
                Date = date,
                GenreId = _unit.Genres.FindGenre(name).Result.Id,
                Name = name,
            };
            const int number = 1;
             _unit.Shows.Create(entityShow).Wait();
            var entityTicket = new Ticket
            {
                Price = number,
                Row = number,
                Seat = number,
                Show = entityShow,
                StatusId = (int) StatusEnum.Available
            };
            _unit.Tickets.Create(entityTicket).Wait();
        }
        
        [Test]
        public void Create_AddingTicket_CheckingCreation()
        {
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10,
                TimeSpan.Zero);

            const string test = "ForTestTicket";
            var authorEntity = new Author
            {
                FirstName = test,
                LastName = test
            };
            var genreEntity = new Genre
            {
                Name = test,
            };

            using var context = new PlaybillDbContext(ContextOptions);
            
            _unit.Authors.Create(authorEntity).Wait();
            _unit.Genres.Create(genreEntity).Wait();

            var author =  _unit.Authors.FindAuthor(test, test).Result;
            var genre = _unit.Genres.FindGenre(test).Result;

            var show = new Show
            {
                Name = test,
                Author = author,
                Date = date,
                GenreId = genre.Id
            };
             _unit.Shows.Create(show).Wait();

            var shows = _unit.Shows.FindByDate(date).Result;
            show = shows.Find(x => x.Author == author && x.Genre == genre);

            const int number = 1;
            
            var ticket = new TicketModel
            {
                Price = number,
                Row = number,
                Seat = number,
                ShowId = show.Id,
                StatusId = (int) StatusEnum.Available,
            };
            _service.Create(ticket).Wait();

            var entity =  context.Tickets.Where(x => x.Price == number
                                                          && x.Row == number && x.Seat == number && x.ShowId == show.Id
                                                          && x.StatusId == (int) StatusEnum.Available)
                .FirstOrDefaultAsync().Result;
            
            
            Assert.NotNull(entity);
        }

        [Test]
        public void Create_AddingExistingTicket_GetCreationException()
        {
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10,
                TimeSpan.Zero);
            var shows = _unit.Shows.FindByDate(date).Result;
            var show = shows[0];
            const int number = 1;
            var ticket = new TicketModel
            {
                Price = number,
                Seat = number,
                Row = number,
                ShowId = show.Id,
                StatusId = (int) StatusEnum.Available,
            };

            Assert.ThrowsAsync<CreationException>(() => _service.Create(ticket));
        }

        [Test]
        public void SellTicket_ChangeExistingTicket_checkingOK()
        {
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10,
                TimeSpan.Zero);
            var shows =  _unit.Shows.FindByDate(date).Result;
            var show = shows[0];
            const int number = 1;
            var ticket = new TicketModel
            {
                Price = number,
                Seat = number,
                Row = number,
                ShowId = show.Id,
                StatusId = (int) StatusEnum.Available,
            };
            _service.SellTicket(ticket).Wait();
            using var context = new PlaybillDbContext(ContextOptions);
            var entity = context.Tickets.Where(x => x.Price == number
                                                          && x.Row == number && x.Seat == number && x.ShowId == show.Id
                                                          && x.StatusId == (int) StatusEnum.Sold)
                .FirstOrDefaultAsync().Result;
            
            Assert.NotNull(entity);
        }
        [Test]
        public void BookTicket_ChangeExistingTicket_checkingOK()
        {
            var date = new DateTimeOffset(1010, 10, 10, 10, 10, 10,
                TimeSpan.Zero);
            var shows = _unit.Shows.FindByDate(date).Result;
            var show = shows[0];
            const int number = 1;
            var ticket = new TicketModel
            {
                Price = number,
                Seat = number,
                Row = number,
                ShowId = show.Id,
                StatusId = (int) StatusEnum.Available,
            };
            _service.BookTicket(ticket).Wait();
            using var context = new PlaybillDbContext(ContextOptions);
            var entity = context.Tickets.Where(x => x.Price == number
                                                          && x.Row == number && x.Seat == number && x.ShowId == show.Id
                                                          && x.StatusId == (int) StatusEnum.Booked)
                .FirstOrDefaultAsync().Result;
            
            Assert.NotNull(entity);
        }
    }
}