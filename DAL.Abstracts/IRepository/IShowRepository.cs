using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Abstracts.IRepository.Base;
using DAL.Entities;

namespace DAL.Abstracts.IRepository
{
    public interface IShowRepository : IBaseRepository<int, Show>
    {
        public Task<List<Show>> FindByAuthorId(int id);
        public Task<List<Show>> FindByGenreId(int id);
        public Task<List<Show>> FindByDate(DateTimeOffset date);
    }
}