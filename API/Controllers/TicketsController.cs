using System.Threading.Tasks;
using BLL.Abstracts.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Base;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketsController: ControllerBase
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }
        
        /// <summary>
        /// Get all tickets.
        /// </summary>
        /// <returns>list of tickets</returns>
        /// <response code="200">Returns all tickets</response>
        /// <response code="500">If the item is null</response>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
       
        /// <summary>
        /// Get an ticket by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>an ticket</returns>
        /// <response code="200">Returns the ticket</response>
        /// <response code="404">If the item is not found</response>
        [HttpGet]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            return Ok(await _service.GetById(id));
        }

        /// <summary>
        /// Post a new ticket.
        /// </summary>
        /// <returns>an created ticket</returns>
        /// <response code="201">Created ticket</response>
        /// <response code="500">If unable to create</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> Add([FromBody]TicketModel ticketModel)
        {
            await _service.Create(ticketModel);
            return CreatedAtAction(nameof(Add), ticketModel);
        }
        /// <summary>
        /// Delete an ticket.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>success of operation</returns>
        /// <response code="204">Success</response>
        /// <response code="500">If unable to delete</response>
        
        [HttpDelete]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.GetById(id);
            return NoContent();
        }
        /// <summary>
        /// Put ticket model to change its status to booked.
        /// </summary>
        /// <returns>success of operation</returns>
        /// <response code="204">Success</response>
        /// <response code="500">If unable to change status</response>
        [HttpPut]
        [Produces("application/json")]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult>Book([FromBody]TicketModel ticketModel)
        {
            await _service.BookTicket(ticketModel);
            return NoContent();
        }
        
        /// <summary>
        /// Put ticket model to change its status to bought.
        /// </summary>
        /// <returns>success of operation</returns>
        /// <response code="204">Success</response>
        /// <response code="500">If unable to change status</response>
        [HttpPut]
        [Produces("application/json")]
        [Route("buy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult>Buy([FromBody]TicketModel ticketModel)
        {
            await _service.SellTicket(ticketModel);
            return NoContent();
        }
    }
}