
using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class TicketUpdateMapper: IUpdateMapper<Ticket,TicketModel>
    {
        public Ticket MapUpdate(TicketModel model, Ticket entity)
        {
            entity.StatusId = model.StatusId;
            entity.Owner = model.Owner;
            return entity;
        }
    }
}