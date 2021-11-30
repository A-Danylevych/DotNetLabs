using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Abstracts.IService;
using Models.Base;
using Configuration;


namespace ConsoleApp
{
    public class ConsoleApp
    {
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IShowService _showService;
        private readonly ITicketService _ticketService;

        public ConsoleApp()
        {
            IServiceProvider utils = new Utils();
            _authorService = (IAuthorService)utils.GetService(typeof(IAuthorService));
            _genreService = (IGenreService) utils.GetService(typeof(IGenreService));
            _showService = (IShowService) utils.GetService(typeof(IShowService));
            _ticketService = (ITicketService) utils.GetService(typeof(ITicketService));
        }

        public void Run()
        {
            Console.WriteLine("Playbill\n1. Add new author\n2. Add new Genre\n3. Add new Show\n4. Add new Ticket\n" +
                              "5. Sell ticket\n6. Book ticket\n7. Quit\n8. Get author id\n9. Get genre id\n10. " +
                              "UnionNext\n11. FindByAuthor\n12. FindByGenre\n13. FindByDate");
            var run = true;
            var unionNext = false;
            var shows = new List<ShowModel>();
            while (run)
            {
                try
                {
                    switch (Console.ReadLine())
                    {
                        case "1":
                            var author = ReadAuthor();
                            _authorService.Create(author).Wait();
                            break;
                        case "2":
                            var genre = ReadGenre();
                            _genreService.Create(genre).Wait();
                            break;
                        case "3":
                            var show = ReadShow();
                            _showService.Create(show).Wait();
                            break;
                        case "4":
                            var ticket = ReadTicket();
                            _ticketService.Create(ticket).Wait();
                            break;
                        case "5":
                            ticket = ReadTicket();
                            Console.WriteLine(_ticketService.SellTicket(ticket).Result);
                            break;
                        case "6":
                            ticket = ReadTicket();
                            Console.WriteLine(_ticketService.BookTicket(ticket).Result);
                            break;
                        case "7":
                            run = false;
                            break;
                        case "8" :
                            author = ReadAuthor();
                            Console.WriteLine(_authorService.GetId(author).Result);
                            break;
                        case "9":
                            genre = ReadGenre();
                            Console.WriteLine(_genreService.GetId(genre).Result);
                            break;
                        case "10":
                            unionNext = true;
                            break;
                        case "11":
                            author = ReadAuthor();
                            if (unionNext)
                            {
                                var next = new List<ShowModel>(_showService.FindByAuthor(author).Result);
                                shows = new List<ShowModel>(shows.Union(next));
                            }
                            else
                            {
                                shows = new List<ShowModel>(_showService.FindByAuthor(author).Result);
                            }
                            PrintShows(shows);
                            break;
                        case "12":
                            var genres = ReadGenres();
                            if (unionNext)
                            {
                                var next = new List<ShowModel>(_showService.FindByGenres(genres).Result);
                                shows = new List<ShowModel>(shows.Union(next));
                            }
                            else
                            {
                                shows = new List<ShowModel>(_showService.FindByGenres(genres).Result);
                            }
                            PrintShows(shows);
                            break;
                        case "13":
                            var date = DateTimeOffset.Parse(Console.ReadLine());
                            if (unionNext)
                            {
                                var next = new List<ShowModel>(_showService.FindByDate(date).Result);
                                shows = new List<ShowModel>(shows.Union(next));
                            }
                            else
                            {
                                shows = new List<ShowModel>(_showService.FindByDate(date).Result);
                            }
                            PrintShows(shows);
                            break;
                        default:
                            Console.WriteLine("Something wrong, try again");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void PrintShows(List<ShowModel> showModels)
        {
            foreach (var model in showModels)
            {
                Console.WriteLine(model);
            }
        }
        private static AuthorModel ReadAuthor()
        {
            var authorModel = new AuthorModel();
            Console.WriteLine("Write author name");
            authorModel.FirstName = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Write author lastname");
            authorModel.LastName = Console.ReadLine() ?? string.Empty;
            return authorModel;
        }
        private static GenreModel ReadGenre()
        {
            var genreModel = new GenreModel();
            Console.WriteLine("Write genre name");
            genreModel.Name = Console.ReadLine() ?? string.Empty;
            return genreModel;
        }

        private static ShowModel ReadShow()
        {
            var show = new ShowModel();
            Console.WriteLine("Write show name");
            show.Name = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Write author id");
            show.AuthorId = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Write genres count");
            var count = int.Parse(Console.ReadLine() ?? string.Empty);
            show.GenresIds = new List<int>();
            for (var i = 0; i < count; i++)
            {
                Console.WriteLine("Write genre id");
                show.GenresIds.Add(int.Parse(Console.ReadLine() ?? string.Empty));
            }
            Console.WriteLine("Write date");
            show.Date = DateTimeOffset.Now;
            return show;
        }

        private static TicketModel ReadTicket()
        {
            var ticket = new TicketModel();
            Console.WriteLine("Write seat");
            ticket.Seat = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Write row");
            ticket.Row = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Write price");
            ticket.Price = decimal.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Write status id");
            ticket.StatusId = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Write show id");
            ticket.ShowId = int.Parse(Console.ReadLine() ?? string.Empty);
            return ticket;
        }

        private static List<GenreModel> ReadGenres()
        {
            var genres = new List<GenreModel>();
            Console.WriteLine("Write genres count");
            var count = int.Parse(Console.ReadLine() ?? string.Empty);
            for (var i = 0; i < count; i++)
            {
                genres.Add(ReadGenre());
            }

            return genres;
        }

    }
}