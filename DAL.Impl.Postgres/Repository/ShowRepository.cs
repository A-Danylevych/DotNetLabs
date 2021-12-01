using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Abstracts.IRepository;
using DAL.Entities;
using DAL.Impl.Postgres.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.Postgres.Repository
{
    public class ShowRepository : BaseRepository<int, Show>, IShowRepository
    {
        public ShowRepository(PlaybillDbContext context) : base(context)
        {
        }

        public async Task<List<Show>> FindByAuthorId(int id)
        {
            var shows = await DbSet.Where(s => s.AuthorId == id)
                .Include(s=> s.Genre).ToListAsync();
            return shows;
        }

        public async Task<List<Show>> FindByAuthor(string name, string surname)
        {
            var shows = await (from show in Context.Shows
                from author in Context.Authors
                where author.Id == show.AuthorId
                where author.FirstName.Contains(name) || author.LastName.Contains(surname)
                select show).ToListAsync();
            return shows;
        }

        public async Task<List<Show>> FindByGenreId(int id)
        {
            return await (from genre in Context.Genres
                where genre.Id == id
                from show in DbSet
                select show).Include(s=> s.Genre).ToListAsync();
        }

        public async Task<List<Show>> FindByDate(DateTimeOffset date)
        {
            return await DbSet.Where(show => show.Date == date).Include(s=> s.Genre)
                .ToListAsync();
        }
    }
}