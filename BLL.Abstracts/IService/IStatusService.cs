using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface IStatusService
    {
        public Task<StatusModel> GetById(int id);
        public Task<ICollection<StatusModel>> GetAll();
    }
}