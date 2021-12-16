using System.Threading.Tasks;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Base;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }
        /// <summary>
        /// Get all genres.
        /// </summary>
        /// <returns>list of genres</returns>
        /// <response code="200">Returns all genres</response>
        /// <response code="500">If the item is null</response>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        /// <summary>
        /// Get a genre by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a genre</returns>
        /// <response code="200">Returns the genre</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _service.GetById(id));
        }


        /// <summary>
        /// Post a new genre.
        /// </summary>
        /// <returns>an created genre</returns>
        /// <response code="201">Created genre</response>
        /// <response code="500">If unable to create</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add([FromBody]GenreModel genreModel)
        {
            await _service.Create(genreModel);
            return CreatedAtAction(nameof(Add), genreModel);
        }
        
        /// <summary>
        /// Delete an genre.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>success of operation</returns>
        /// <response code="204">Success</response>
        /// <response code="500">If unable to delete</response>
        
        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}