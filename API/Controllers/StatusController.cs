
using System.Threading.Tasks;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _service;

        public StatusController(IStatusService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all statuses.
        /// </summary>
        /// <returns>list of statuses</returns>
        /// <response code="200">Returns all statuses</response>
        /// <response code="500">If the item is null</response>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
        /// <summary>
        /// Get an status by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an status</returns>
        /// <response code="200">Returns the status</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _service.GetById(id));
        }
    }
}