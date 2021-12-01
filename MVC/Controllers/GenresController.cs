using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Base;

namespace MVC.Controllers
{
    public class GenresController : Controller
    {
        private IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(GenreModel genreModel)
        {
            await _service.Create(genreModel);
            return RedirectToAction("Index");
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Info(int id)
        {
            return View(await _service.GetById(id));
        }
    }
}