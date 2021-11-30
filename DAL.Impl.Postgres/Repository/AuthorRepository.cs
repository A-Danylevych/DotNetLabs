using System.Linq;
using System.Threading.Tasks;
using DAL.Abstracts.IRepository;
using DAL.Entities;
using DAL.Impl.Postgres.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.Postgres.Repository
{
    public class AuthorRepository : BaseRepository<int, Author>, IAuthorRepository
    {
        public AuthorRepository(PlaybillDbContext context) : base(context)
        {
        }

        public async Task<Author> FindAuthor(string name, string lastName)
        {
            return await DbSet.Where(author => author.FirstName == name && author.LastName == lastName)
                .FirstOrDefaultAsync();
        }
    }
}