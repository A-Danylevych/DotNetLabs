using DAL.Abstracts;
using DAL.Abstracts.IRepository;
using DAL.Impl.Postgres;
using DAL.Impl.Postgres.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Configuration
{
    public static class DalDependencyInstaller
    {
        public static void AddPostgresDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlaybillDbContext>(opt =>
                opt.UseNpgsql(configuration.GetConnectionString("Postgres")));
            
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IShowRepository, ShowRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();

            services.AddTransient<AbstractUnitOfWork, UnitOfWork>();
        }
    }
}