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
        public async Task<ActionResult> Add([FromBody]TicketModel ticketModel)
        {
            await _service.Create(ticketModel);
            return CreatedAtAction(nameof(Add), ticketModel);
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _service.GetById(id);
            return Ok(id);
        }
        [HttpPut]
        [Produces("application/json")]
        [Route("book")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult>Book([FromBody]TicketModel ticketModel)
        {
            await _service.BookTicket(ticketModel);
            return Ok();
        }
        [HttpPut]
        [Produces("application/json")]
        [Route("buy")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult>Buy([FromBody]TicketModel ticketModel)
        {
            await _service.SellTicket(ticketModel);
            return Ok();
        }
    }
}