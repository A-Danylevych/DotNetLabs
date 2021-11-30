using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class TicketMapper: IMapper<Ticket, TicketModel>
    {
        public TicketModel Map(Ticket entity)
        {
            return new TicketModel
            {
                Price = entity.Price,
                Row = entity.Row,
                Seat = entity.Seat,
                ShowId = entity.ShowId,
                StatusId = entity.StatusId,
            };
        }
    }
}