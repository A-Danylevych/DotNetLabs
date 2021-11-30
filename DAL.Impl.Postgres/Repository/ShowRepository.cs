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
                .Include(s=> s.Genres).ToListAsync();
            return shows;
        }
        
        public async Task<List<Show>> FindByGenreIds(ICollection<int> ids)
        {
            return await (from genre in Context.Genres
                where ids.Contains(genre.Id)
                from show in DbSet
                select show).Include(s=> s.Genres).ToListAsync();
        }

        public async Task<List<Show>> FindByDate(DateTimeOffset date)
        {
            return await DbSet.Where(show => show.Date == date).Include(s=> s.Genres)
                .ToListAsync();
        }
    }
}