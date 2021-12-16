using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Base;
using Models.Status;
using Newtonsoft.Json;

namespace MVC.Controllers
{
    public class ShowsController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly ITicketService _ticketService;
        private readonly IShowService _service;

        public ShowsController(ITicketService ticketService, IGenreService genreService, 
            IAuthorService authorService, IShowService service)
        {
            _ticketService = ticketService;
            _genreService = genreService;
            _authorService = authorService;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        public IActionResult Add()
        {
            var authorList = _authorService.GetAll().Result;
            var genreList = _genreService.GetAll().Result;
            var authors =  new SelectList(authorList, "Id",
                "LastName" );
            var genres= new SelectList(genreList, "Id", "Name");
            ViewBag.Authors = authors;
            ViewBag.Genres = genres;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ShowModel showModel)
        {
            await _service.Create(showModel);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult FindByAuthor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FindByAuthor(AuthorModel authorModel)
        {
            ICollection<ShowModel> list;
            try
            {
                list = await _service.FindByAuthor(authorModel);
            }
            catch
            {
                return RedirectToAction("NotFound");
            }

            TempData["Shows"] = JsonConvert.SerializeObject(list);
            return RedirectToAction("Found");
        }
        [HttpGet]
        public IActionResult FindByGenre()
        {
            var genreList = _genreService.GetAll().Result;
            var genres= new SelectList(genreList, "Id", "Name");
            ViewBag.Genres = genres;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> FindByGenre(GenreModel genre)
        {
            ICollection<ShowModel> list;
            try
            {
                list = await _service.FindByGenre(genre);
            }
            catch
            {
               return RedirectToAction("NotFound");
            }

            TempData["Shows"] = JsonConvert.SerializeObject(list);
            return RedirectToAction("Found");
        }

        public IActionResult Found()
        {
            var showModels = JsonConvert.DeserializeObject<List<ShowModel>>((string)TempData["Shows"]);
            return View(showModels);
        }

        public IActionResult NotFound()
        {
            return View();
        }

        public async Task<IActionResult> AddTickets(int showId)
        {
            TempData["showId"] = showId;
            var created = await _ticketService.Created(showId);
            TempData["created"] = created;
            return View(created);
        }

        [HttpPost]
        public async Task<IActionResult> AddTickets(int rowsCount, int seatCount, int basePrice)
        {
            var showId = (int) TempData["showId"];
            var created = (bool) TempData["created"];
            if (created)
            {
                await _ticketService.Delete(showId);
            }
            await _ticketService.CreateTicketsForShow(showId, rowsCount, seatCount, basePrice);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> TicketInfo(int showId, string nextAction)
        {
            TempData["showId"] = showId;
            var price = await _ticketService.GetPrice(showId);
            ViewBag.NextAction = nextAction;
            return View(price);
        }

        public async Task<IActionResult> BuyTicket(TicketModel ticketModel)
        {
            ticketModel.ShowId = (int) TempData["showId"];
            var exist = await _ticketService.Created(ticketModel.ShowId, ticketModel.Row, ticketModel.Seat);
            if (!exist)
            {
                return RedirectToAction("TicketNotFound");
            }

            ticketModel.StatusId = (int)StatusEnum.Available;
            var newModel = await _ticketService.SellTicket(ticketModel);
            TempData["Owner"] = ticketModel.Owner;
            return RedirectToAction(newModel.StatusId != (int) StatusEnum.Sold ? "Sorry" : "ThankYou");
        }

        public async Task<IActionResult> BookTicket(TicketModel ticketModel)
        {
            ticketModel.ShowId = (int) TempData["showId"];
            var exist = await _ticketService.Created(ticketModel.ShowId, ticketModel.Row, ticketModel.Seat);
            if (!exist)
            {
                return RedirectToAction("TicketNotFound");
            }

            ticketModel.StatusId = (int)StatusEnum.Available;
            var newModel = await _ticketService.BookTicket(ticketModel);
            TempData["Owner"] = ticketModel.Owner;
            return RedirectToAction(newModel.StatusId != (int) StatusEnum.Booked ? "Sorry" : "ThankYou");
        }

        public IActionResult TicketNotFound()
        {
            return View();
        }

        public IActionResult ThankYou()
        {
            return View(TempData["Owner"]);
        }

        public IActionResult Sorry()
        {
            return View();
        }
    }
}