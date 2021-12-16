using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface ITicketService
    {
        public Task<TicketModel> SellTicket(TicketModel ticketModel);
        public Task<TicketModel> BookTicket(TicketModel ticketModel);
        
        public Task Create(TicketModel ticketModel);
        public Task CreateTicketsForShow(int showId, int rowCount, int seatCount, decimal basePrice);
        public Task<decimal> GetPrice(int showId);
        public Task<bool> Created(int showId);
        public Task Delete(int showId);
        public Task<bool> Created(int showId, int row, int seat);
        public Task<TicketModel> GetById(int id);
    }
}