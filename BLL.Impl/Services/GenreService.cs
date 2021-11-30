using System.Threading.Tasks;
using BLL.Abstracts.IMapper;
using BLL.Abstracts.IService;
using DAL.Abstracts;
using DAL.Entities;
using Models.Base;
using Models.Error;

namespace BLL.Impl.Services
{
    public class GenreService: IGenreService
    {
        private readonly IBackMapper<Genre, GenreModel> _backMapper;
        private readonly AbstractUnitOfWork _unit;

        public GenreService(IBackMapper<Genre, GenreModel> backMapper, AbstractUnitOfWork unit)
        {
            _backMapper = backMapper;
            _unit = unit;
        }
        
        public async Task Create(GenreModel genreModel)
        {

            var entity = _backMapper.MapBack(genreModel);
            try
            {
                await _unit.Genres.Create(entity);
                await _unit.Save();
            }
            catch
            {
                throw new CreationException(typeof(Genre));
            }
        }

        public async Task<int> GetId(GenreModel genreModel)
        {
            var entity = await _unit.Genres.FindGenre(genreModel.Name);
            if (entity == null)
            {
                throw new NotFoundException(typeof(Genre));
            }

            return entity.Id;
        }
    }
}