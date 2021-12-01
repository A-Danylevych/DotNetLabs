using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface IAuthorService
    {
        public Task Create(AuthorModel authorModel);
        public Task<int> GetId(AuthorModel authorModel);
        public Task<ICollection<AuthorModel>> GetAll();
        public Task Delete(int id);
        public Task<AuthorModel> GetById(int id);
    }
}