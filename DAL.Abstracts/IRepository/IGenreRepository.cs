using System.Threading.Tasks;
using DAL.Abstracts.IRepository.Base;
using DAL.Entities;

namespace DAL.Abstracts.IRepository
{
    public interface IGenreRepository : IBaseRepository<int, Genre>
    {
        public Task<Genre> FindGenre(string name);
    }
}