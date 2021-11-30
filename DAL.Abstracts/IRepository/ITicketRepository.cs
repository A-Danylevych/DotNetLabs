using System.Threading.Tasks;
using DAL.Abstracts.IRepository.Base;
using DAL.Entities;

namespace DAL.Abstracts.IRepository
{
    public interface ITicketRepository:IBaseRepository<int, Ticket>
    {
        public Task<Ticket> Find(Ticket entity);
    }
}