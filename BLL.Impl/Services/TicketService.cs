using System.Linq;
using System.Threading.Tasks;
using BLL.Abstracts.IMapper;
using BLL.Abstracts.IService;
using DAL.Abstracts;
using DAL.Entities;
using Models.Base;
using Models.Error;
using Models.Status;

namespace BLL.Impl.Services
{
    public class TicketService : ITicketService
    {
        private readonly AbstractUnitOfWork _unit;
        private readonly IBackMapper<Ticket, TicketModel> _backMapper;
        private readonly IMapper<Ticket, TicketModel> _mapper;
        private readonly IUpdateMapper<Ticket, TicketModel> _statusUpdateMapper;

        public TicketService(AbstractUnitOfWork unit, IBackMapper<Ticket, TicketModel> backMapper,
            IUpdateMapper<Ticket, TicketModel> statusUpdateMapper, IMapper<Ticket, TicketModel> mapper)
        {
            _unit = unit;
            _backMapper = backMapper;
            _statusUpdateMapper = statusUpdateMapper;
            _mapper = mapper;
        }

        public async Task<TicketModel> SellTicket(TicketModel ticketModel)
        {
            return await SellOrBookTicket(ticketModel, StatusEnum.Sold);
        }

        public async Task<TicketModel> BookTicket(TicketModel ticketModel)
        {
            return await SellOrBookTicket(ticketModel, StatusEnum.Booked);
        }

        public async Task Create(TicketModel ticketModel)
        {
            var entity = _backMapper.MapBack(ticketModel);
            try
            {
                await _unit.Tickets.Create(entity);
            }
            catch
            {
                throw new CreationException(typeof(Ticket));
            }
        }

        public async Task CreateTicketsForShow(int showId, int rowCount, int seatCount, decimal price)
        {
            for (var row = 1; row <= rowCount; row++)
            {
                for (var seat = 1; seat < +seatCount; seat++)
                {
                    var entity = new Ticket
                    {
                        Price = price,
                        Row = row,
                        Seat = seat,
                        StatusId = (int) StatusEnum.Available,
                        ShowId = showId
                    };
                    await _unit.Tickets.Create(entity);
                }
            }

            await _unit.Save();
        }

        public async Task<decimal> GetPrice(int showId)
        {

            var list = await _unit.Tickets.Find(showId);
            if (list.Count == 0)
            {
                throw new NotFoundException(typeof(Ticket));
            }

            return list.First().Price;
        }

        public async Task<bool> Created(int showId)
        {
            var list = await _unit.Tickets.Find(showId);
            return list.Count != 0;
        }

        public async Task Delete(int showId)
        {
            var list = await _unit.Tickets.Find(showId);
            foreach (var ticket in list)
            {
                _unit.Tickets.Delete(ticket);
            }

            await _unit.Save();
        }

        public async Task<bool> Created(int showId, int row, int seat)
        {
            var list = await _unit.Tickets.Find(showId);
            var element = list.FirstOrDefault(x => x.Row == row && x.Seat == seat);
            return element != null;
        }

        private async Task<TicketModel> SellOrBookTicket(TicketModel ticketModel, StatusEnum status)
        {
            var entity = _backMapper.MapBack(ticketModel);
            entity = await _unit.Tickets.Find(entity);
            if (entity == null)
            {
                throw new NotFoundException(typeof(Ticket));
            }

            if (entity.Owner != null && entity.Owner != ticketModel.Owner) return ticketModel;
            if (status == StatusEnum.Booked && entity.StatusId == (int)StatusEnum.Sold || 
                entity.StatusId == (int) status) return ticketModel;
            ticketModel.StatusId = (int) status;
            entity = _statusUpdateMapper.MapUpdate(ticketModel, entity);
            _unit.Tickets.Update(entity);
            await _unit.Save();

            return _mapper.Map(entity);
        }
        
        public async Task<TicketModel> GetById(int id)
        {
            return _mapper.Map(await _unit.Tickets.GetById(id));
        }
    }
}