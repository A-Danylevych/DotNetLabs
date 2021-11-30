using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface ITicketService
    {
        public Task<TicketModel> SellTicket(TicketModel ticketModel);
        public Task<TicketModel> BookTicket(TicketModel ticketModel);
        
        public Task Create(TicketModel ticketModel);
    }
}