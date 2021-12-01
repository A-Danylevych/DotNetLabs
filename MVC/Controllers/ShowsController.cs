using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Mvc;
using Models.Base;
using Controller = Microsoft.AspNetCore.Mvc.Controller;

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

        public async Task<IActionResult> Add()
        {
            var authors =  new SelectList(await _authorService.GetAll(), "Name", "LastName");
            var genres= new Microsoft.AspNetCore.Mvc.Rendering.SelectList(await _genreService.GetAll(), 
                "Name");
            ViewBag.Authors = authors;
            ViewBag.Genres = genres;
            return View();
        }

        public IActionResult Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}