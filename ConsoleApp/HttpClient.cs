using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using Models.Base;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ConsoleApp
{
    public static class ConsoleHttpClient
    {
        private const string AppPath= "https://localhost:44369";
        public static string CreateAuthor(AuthorModel author)
        {
            using var client = new HttpClient();
            var response = client.PostAsJsonAsync(AppPath + "/Authors", author).Result;
            return response.StatusCode.ToString();
        }

        public static string CreateGenre(GenreModel genre)
        {
            using var client = new HttpClient();
            var response = client.PostAsJsonAsync(AppPath + "/Genres", genre).Result;
            return response.StatusCode.ToString();
        }

        public static string CreateShow(ShowModel show)
        {
            using var client = new HttpClient();
            var response = client.PostAsJsonAsync(AppPath + "/Shows", show).Result;
            return response.StatusCode.ToString();
        }

        public static string CreateTicket(TicketModel ticket)
        {
            using var client = new HttpClient();
            var response = client.PostAsJsonAsync(AppPath + "/Tickets", ticket).Result;
            return response.StatusCode.ToString();
        }

        public static string SellTicket(TicketModel ticket)
        {
            using var client = new HttpClient();
            var response = client.PutAsJsonAsync(AppPath + $"/Tickets/sell", ticket).Result;
            return response.StatusCode.ToString();
        }

        public static string BookTicket(TicketModel ticket)
        {
            using var client = new HttpClient();
            var response = client.PutAsJsonAsync(AppPath + "/Tickets/book", ticket).Result;
            return response.StatusCode.ToString();
        }

        public static string GetAllGenres()
        {
            using var client = new HttpClient();
            var response = client.GetAsync(AppPath + $"/Genres").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<GenreModel>>(str);
            const string result = "";
            return list == null ? result : list.Aggregate(result, (current, genre) => current + (genre.ToString() 
                + "\n"));
        }

        public static string GetAllAuthors()
        {
            using var client = new HttpClient();
            var response = client.GetAsync(AppPath + $"/Authors").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<AuthorModel>>(str);
            const string result = "";
            return list == null ? result : list.Aggregate(result, (current, item) => current
                + (item + "\n"));
        }

        public static string GetAllShows()
        {
            using var client = new HttpClient();
            var response = client.GetAsync(AppPath + $"/Shows").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<ShowModel>>(str);
            const string result = "";
            return list == null ? result : list.Aggregate(result, (current, item) => current
                + (item + "\n"));
        }
        public static string GetAllTickets()
        {
            using var client = new HttpClient();
            var response = client.GetAsync(AppPath + $"/Tickets").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<TicketModel>>(str);
            const string result = "";
            return list == null ? result : list.Aggregate(result, (current, item) => current
                + (item + "\n"));
        }

        public static string FindByGenre(GenreModel genre)
        {
            using var client = new HttpClient();
            var response = client.PostAsJsonAsync(AppPath + $"/Shows/ByGenre", genre).Result;
            var str = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<ShowModel>>(str);
            const string result = "";
            return list == null ? result : list.Aggregate(result, (current, item) => current
                + (item + "\n"));
        }

        public static string FindByAuthor(AuthorModel author)
        {
            using var client = new HttpClient();
            var response = client.PostAsJsonAsync(AppPath + $"/Shows/ByAuthor", author).Result;
            var str = response.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<ShowModel>>(str);
            const string result = "";
            return list == null ? result : list.Aggregate(result, (current, item) => current
                + (item + "\n"));
        }

        public static string DeleteAuthor(int id)
        {
            using var client = new HttpClient();
            var response = client.DeleteAsync(AppPath + $"/Authors/{id}").Result;
            return response.StatusCode.ToString();
        }
        public static string DeleteGenre(int id)
        {
            using var client = new HttpClient();
            var response = client.DeleteAsync(AppPath + $"/Genres/{id}").Result;
            return response.StatusCode.ToString();
        }
        public static string DeleteShow(int id)
        {
            using var client = new HttpClient();
            var response = client.DeleteAsync(AppPath + $"/Shows/{id}").Result;
            return response.StatusCode.ToString();
        }
        public static string DeleteTicket(int id)
        {
            using var client = new HttpClient();
            var response = client.DeleteAsync(AppPath + $"/Tickets/{id}").Result;
            return response.StatusCode.ToString();
        }
    }
}