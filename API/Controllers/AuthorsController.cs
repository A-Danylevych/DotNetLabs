using System.Threading.Tasks;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Base;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Get all authors.
        /// </summary>
        /// <returns>list of authors</returns>
        /// <response code="200">Returns all authors</response>
        /// <response code="500">If the item is null</response>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        
        /// <summary>
        /// Get an author by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an author</returns>
        /// <response code="200">Returns the author</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _service.GetById(id));
        }

        /// <summary>
        /// Post a new author.
        /// </summary>
        /// <returns>an created author</returns>
        /// <response code="201">Created author</response>
        /// <response code="500">If unable to create</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Add([FromBody]AuthorModel authorModel)
        {
            await _service.Create(authorModel);
            return CreatedAtAction(nameof(Add), authorModel);
        }
        
        /// <summary>
        /// Delete an author.
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
            await _service.GetById(id);
            return NoContent();
        }
    }
}