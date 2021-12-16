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

        public ConsoleApp()
        {
            IServiceProvider utils = new Utils();
        }

        public void Run()
        {
            Console.WriteLine("Playbill\n1. Add new author\n2. Add new Genre\n3. Add new Show\n4. Add new Ticket\n" +
                              "5. Sell ticket\n6. Book ticket\n7. Get All shows\n8. Get All authors\n9. Get All genres" +
                              "\n10. FindByGenre\n11. FindByAuthor\n12. Quit");
            var run = true;
            var shows = new List<ShowModel>();
            while (run)
            {
                try
                {
                    GenreModel genre;
                    switch (Console.ReadLine())
                    {
                        case "1":
                            var author = ReadAuthor();
                            Console.WriteLine(ConsoleHttpClient.CreateAuthor(author));
                            break;
                        case "2":
                            genre = ReadGenre();
                            Console.WriteLine(ConsoleHttpClient.CreateGenre(genre));
                            break;
                        case "3":
                            var show = ReadShow();
                            Console.WriteLine(ConsoleHttpClient.CreateShow(show));
                            break;
                        case "4":
                            var ticket = ReadTicket();
                            Console.WriteLine(ConsoleHttpClient.CreateTicket(ticket));
                            break;
                        case "5":
                            ticket = ReadTicket();
                            Console.WriteLine(ConsoleHttpClient.SellTicket(ticket));
                            break;
                        case "6":
                            ticket = ReadTicket();
                            Console.WriteLine(ConsoleHttpClient.BookTicket(ticket));
                            break;
                        case "7":
                            Console.WriteLine(ConsoleHttpClient.GetAllShows());
                            break;
                        case "8" :
                            Console.WriteLine(ConsoleHttpClient.GetAllAuthors());
                            break;
                        case "9":
                            Console.WriteLine(ConsoleHttpClient.GetAllGenres());
                            break;
                        case "10":
                            genre = ReadGenre();
                            Console.WriteLine(ConsoleHttpClient.FindByGenre(genre));
                            //PrintShows(shows);
                            break;
                        case "11":
                            author = ReadAuthor();
                            Console.WriteLine(ConsoleHttpClient.FindByAuthor(author));
                            //PrintShows(shows);
                            break;
                        case "12":
                            run = false;
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
            Console.WriteLine("Write genre id");
            show.GenreId = int.Parse(Console.ReadLine() ?? string.Empty);
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

        private static GenreModel ReadGenres()
        {
            var genres = new GenreModel();

            return genres;
        }

    }
}