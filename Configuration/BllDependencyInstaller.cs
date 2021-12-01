using BLL.Abstracts.IMapper;
using BLL.Abstracts.IService;
using BLL.Impl.Mappers;
using BLL.Impl.Services;
using DAL.Entities;
using Microsoft.Extensions.DependencyInjection;
using Models.Base;

namespace Configuration
{
    public static class BllDependencyInstaller
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBackMapper<Author, AuthorModel>, AuthorBackMapper>();
            services.AddTransient<IMapper<Author, AuthorModel>, AuthorMapper>();
            
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IBackMapper<Genre, GenreModel>, GenreBackMapper>();
            services.AddTransient<IMapper<Genre, GenreModel>, GenreMapper>();

            services.AddTransient<IShowService, ShowService>();
            services.AddTransient<IBackMapper<Show, ShowModel>, ShowBackMapper>();
            services.AddTransient<IMapper<Show, ShowModel>, ShowMapper>();
            
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IBackMapper<Ticket, TicketModel>, TicketBackMapper>();
            services.AddTransient<IMapper<Ticket, TicketModel>, TicketMapper>();
            services.AddTransient<IUpdateMapper<Ticket, TicketModel>, TicketUpdateMapper>();
        }
    }
}