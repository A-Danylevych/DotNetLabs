using System.Threading.Tasks;
using DAL.Abstracts.IRepository;

namespace DAL.Abstracts
{
    public abstract class AbstractUnitOfWork
    {
        public abstract IAuthorRepository Authors { get; }
        public abstract IGenreRepository Genres { get; }
        public abstract IShowRepository Shows { get;  }
        public abstract ITicketRepository Tickets { get; }
        public abstract Task Save();
    }
}