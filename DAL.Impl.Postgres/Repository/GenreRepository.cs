using System.Linq;
using System.Threading.Tasks;
using DAL.Abstracts.IRepository;
using DAL.Entities;
using DAL.Impl.Postgres.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.Postgres.Repository
{
    public class GenreRepository: BaseRepository<int,Genre>, IGenreRepository
    {
        public GenreRepository(PlaybillDbContext context) : base(context)
        {
        }

        public async Task<Genre> FindGenre(string name)
        {
            return await DbSet.Where(genre => genre.Name == name).FirstOrDefaultAsync();
        }
    }
}