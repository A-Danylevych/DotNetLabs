using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface IAuthorService
    {
        public Task Create(AuthorModel authorModel);
        public Task<int> GetId(AuthorModel authorModel);
    }
}