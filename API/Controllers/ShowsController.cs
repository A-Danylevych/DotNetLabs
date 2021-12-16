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

        /// <summary>
        /// Get all shows.
        /// </summary>
        /// <returns>list of shows</returns>
        /// <response code="200">Returns all shows</response>
        /// <response code="500">If the item is null</response>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        /// <summary>
        /// Get a show by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a show</returns>
        /// <response code="200">Returns the show</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _service.GetById(id));
        }

        /// <summary>
        /// Post a new show.
        /// </summary>
        /// <returns>an created show</returns>
        /// <response code="201">Created show</response>
        /// <response code="500">If unable to create</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody]ShowModel showModel)
        {
            await _service.Create(showModel);
            return CreatedAtAction(nameof(Add), showModel);
        }
        
        /// <summary>
        /// Get all shows with some author.
        /// </summary>
        /// <returns>list of shows</returns>
        /// <response code="200">Returns the specific shows</response>
        /// <response code="500">If the item is null</response>
        [HttpPost]
        [Route("ByAuthor")]
        [Produces("application/json")]
        public async Task<IActionResult> FindByAuthor([FromBody]AuthorModel authorModel)
        {
            ICollection<ShowModel> list = await _service.FindByAuthor(authorModel);
            return Ok(list);
        }
        /// <summary>
        /// Get all shows with some genre.
        /// </summary>
        /// <returns>list of shows</returns>
        /// <response code="200">Returns the specific shows</response>
        /// <response code="500">If the item is null</response>
        [HttpPost]
        [Route("ByGenre")]
        [Produces("application/json")]
        public async Task<IActionResult> FindByGenre([FromBody]GenreModel genre)
        {
            var list = await _service.FindByGenre(genre);
            return Ok(list);
        }
        
        /// <summary>
        /// Delete an show.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>success of operation</returns>
        /// <response code="204">Success</response>
        /// <response code="500">If unable to delete</response>
        [HttpDelete]
        [Route("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
        
    }
}