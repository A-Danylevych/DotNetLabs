using System.Linq;
using System.Threading.Tasks;
using DAL.Abstracts.IRepository;
using DAL.Entities;
using DAL.Impl.Postgres.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.Postgres.Repository
{
    public class TicketRepository: BaseRepository<int, Ticket>, ITicketRepository
    {
        public TicketRepository(PlaybillDbContext context) : base(context)
        {
        }

        public async Task<Ticket> Find(Ticket entity)
        {
            return await DbSet.Where(ticket =>
                    ticket.Row == entity.Row && ticket.Seat == entity.Seat
                                             && ticket.ShowId == entity.ShowId)
                .FirstOrDefaultAsync();
        }
    }
}