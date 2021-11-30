using System.Threading.Tasks;
using DAL.Abstracts.IRepository.Base;
using DAL.Entities;

namespace DAL.Abstracts.IRepository
{
    public interface IStatusRepository: IBaseRepository<int, Status>
    {
        public Task<Status> GetStatusByName(string name);
    }
}