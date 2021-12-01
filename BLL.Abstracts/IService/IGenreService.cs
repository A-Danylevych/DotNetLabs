using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface IGenreService
    {
        public Task Create(GenreModel genreModel);
        public Task<int> GetId(GenreModel genreModel);
        Task<ICollection<GenreModel>> GetAll();
        public Task Delete(int id);
        public Task<GenreModel> GetById(int id);
    }
}