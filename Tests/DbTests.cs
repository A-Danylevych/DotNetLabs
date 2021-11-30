using System.Threading.Tasks;
using DAL.Impl.Postgres;
using Microsoft.EntityFrameworkCore;
using Models.Status;
using NUnit.Framework;
using Status = DAL.Entities.Status;

namespace Tests
{
    public class DbTests
    {
        protected DbTests(DbContextOptions<PlaybillDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<PlaybillDbContext> ContextOptions { get; }
        
        [TearDown]
        public void TearDown()
        {
            using var context = new PlaybillDbContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.SaveChanges();
        }

        private void Seed()
        {
            using var context = new PlaybillDbContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
            context.SaveChanges();
        }
    }
}