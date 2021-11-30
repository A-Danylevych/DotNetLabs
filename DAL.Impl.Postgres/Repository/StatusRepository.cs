using System.Linq;
using System.Threading.Tasks;
using DAL.Abstracts.IRepository;
using DAL.Entities;
using DAL.Impl.Postgres.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.Postgres.Repository
{
    public class StatusRepository: BaseRepository<int, Status>, IStatusRepository
    {
        public StatusRepository(PlaybillDbContext context) : base(context)
        {
        }

        public async Task<Status> GetStatusByName(string name)
        {
            return await DbSet.Where(status => status.Name == name).FirstOrDefaultAsync();
        }
    }
}