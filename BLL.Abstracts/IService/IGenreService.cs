using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface IGenreService
    {
        public Task Create(GenreModel genreModel);
        public Task<int> GetId(GenreModel genreModel);
    }
}