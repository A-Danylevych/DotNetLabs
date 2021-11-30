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

        public async Task<ICollection<ShowModel>> FindByGenres(IEnumerable<GenreModel> genres)
        {
            var ids = new List<int>();
            foreach (var genre in genres)
            {
                ids.Add(await _genreService.GetId(genre));
            }

            var shows = await _unit.Shows.FindByGenreIds(ids);
            var result = shows.Select(show => _mapper.Map(show)).ToList();
            return result;
        }

        public async Task<ICollection<ShowModel>> FindByDate(DateTimeOffset date)
        {
            var shows = await _unit.Shows.FindByDate(date);
            var result = shows.Select(show => _mapper.Map(show)).ToList();
            return result;
        }
    }
}