using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface IShowService
    {
        public Task Create(ShowModel showModel);
        public Task<List<ShowModel>> FindByAuthor(AuthorModel authorModel);
        public Task<ICollection<ShowModel>> FindByGenres(IEnumerable<GenreModel> genres);
        public Task<ICollection<ShowModel>> FindByDate(DateTimeOffset date);
    }
}