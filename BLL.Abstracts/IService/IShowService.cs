using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Base;

namespace BLL.Abstracts.IService
{
    public interface IShowService
    {
        public Task Create(ShowModel showModel);
        public Task<List<ShowModel>> FindByAuthor(AuthorModel authorModel);
        public Task<ICollection<ShowModel>> FindByGenre(GenreModel genres);
        public Task<ICollection<ShowModel>> FindByDate(DateTimeOffset date);
        public Task<ICollection<ShowModel>> GetAll();
    }
}