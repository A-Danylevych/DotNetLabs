using System;
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
    public class ShowService: IShowService
    {
        private readonly IBackMapper<Show, ShowModel> _backMapper;
        private readonly IMapper<Show, ShowModel> _mapper;
        private readonly AbstractUnitOfWork _unit;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;

        public ShowService(IBackMapper<Show, ShowModel> backMapper, IMapper<Show, ShowModel> mapper, 
            AbstractUnitOfWork unit, IAuthorService authorService, IGenreService genreService)
        {
            _backMapper = backMapper;
            _mapper = mapper;
            _unit = unit;
            _authorService = authorService;
            _genreService = genreService;
        }


        public async Task Create(ShowModel showModel)
        {
            var entity = _backMapper.MapBack(showModel);
            try
            {
                await _unit.Shows.Create(entity);
                await _unit.Save();
            }
            catch
            {
                throw new CreationException(typeof(Show));
            }
        }

        public async Task<List<ShowModel>> FindByAuthor(AuthorModel authorModel)
        {
            var authorId = await _authorService.GetId(authorModel);
            var shows = await _unit.Shows.FindByAuthorId(authorId);
            var result = shows.Select(show => _mapper.Map(show)).ToList();
            return result;
        }

        public async Task<ICollection<ShowModel>> FindByGenre(GenreModel genre)
        {
            var shows = await _unit.Shows.FindByGenreId(genre.Id);
            var result = shows.Select(show => _mapper.Map(show)).ToList();
            return result;
        }

        public async Task<ICollection<ShowModel>> FindByDate(DateTimeOffset date)
        {
            var shows = await _unit.Shows.FindByDate(date);
            var result = shows.Select(show => _mapper.Map(show)).ToList();
            return result;
        }
        public async Task<ICollection<ShowModel>> GetAll()
        {
            return (from show in await _unit.Shows.GetAll() select _mapper.Map(show)).ToList();
        }
        public async Task<ShowModel> GetById(int id)
        {
            return _mapper.Map(await _unit.Shows.GetById(id));
        }
        
        public async Task Delete(int id)
        {
            var entity = await _unit.Shows.GetById(id);
            if (entity == null)
            {
                throw new NotFoundException(typeof(Author));
            }
            _unit.Shows.Delete(entity);
            await _unit.Save();
        }
    }
}