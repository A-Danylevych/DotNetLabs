using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper<Genre, GenreModel> _mapper;

        public GenreService(IBackMapper<Genre, GenreModel> backMapper, AbstractUnitOfWork unit, 
            IMapper<Genre, GenreModel> mapper)
        {
            _backMapper = backMapper;
            _unit = unit;
            _mapper = mapper;
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

        public async Task<ICollection<GenreModel>> GetAll()
        {
            return (from genre in await _unit.Genres.GetAll() select _mapper.Map(genre)).ToList();
        }
        public async Task Delete(int id)
        {
            var entity = await _unit.Genres.GetById(id);
            if (entity == null)
            {
                throw new NotFoundException(typeof(Author));
            }
            _unit.Genres.Delete(entity);
            await _unit.Save();
        }

        public async Task<GenreModel> GetById(int id)
        {
            return _mapper.Map(await _unit.Genres.GetById(id));
        }
    }
}