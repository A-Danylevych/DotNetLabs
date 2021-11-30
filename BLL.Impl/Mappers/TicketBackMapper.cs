using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class TicketBackMapper : IBackMapper<Ticket, TicketModel>
    {
        public Ticket MapBack(TicketModel model)
        {
            return new Ticket
            {
                Price = model.Price,
                Row = model.Row,
                Seat = model.Seat,
                ShowId = model.ShowId,
                StatusId = model.StatusId,
            };
        }
    }
}