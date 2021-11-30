using System.Threading.Tasks;
using DAL.Abstracts.IRepository.Base;
using DAL.Entities;

namespace DAL.Abstracts.IRepository
{
    public interface IAuthorRepository: IBaseRepository<int, Author>
    {
        public Task<Author> FindAuthor(string name, string lastName);
    }
}