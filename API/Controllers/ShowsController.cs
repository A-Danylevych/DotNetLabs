using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Base;
using Models.Status;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShowsController : ControllerBase
    {
        private readonly IShowService _service;

        public ShowsController(IShowService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _service.GetById(id));
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody]ShowModel showModel)
        {
            await _service.Create(showModel);
            return CreatedAtAction(nameof(Add), showModel);
        }
        
        
        [HttpPost]
        [Route("ByAuthor")]
        [Produces("application/json")]
        public async Task<IActionResult> FindByAuthor([FromBody]AuthorModel authorModel)
        {
            ICollection<ShowModel> list = await _service.FindByAuthor(authorModel);
            return Ok(list);
        }
        [HttpPost]
        [Route("ByGenre")]
        [Produces("application/json")]
        public async Task<IActionResult> FindByGenre([FromBody]GenreModel genre)
        {
            var list = await _service.FindByGenre(genre);
            return Ok(list);
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.Delete(id);
            return Ok();
        }
        
    }
}