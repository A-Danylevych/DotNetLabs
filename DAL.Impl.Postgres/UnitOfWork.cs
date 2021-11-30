using System;
using System.Threading.Tasks;
using DAL.Abstracts;
using DAL.Abstracts.IRepository;
using DAL.Impl.Postgres.Repository;

namespace DAL.Impl.Postgres
{
    public class UnitOfWork :AbstractUnitOfWork, IDisposable 
    {
        private readonly PlaybillDbContext _context;
        private IAuthorRepository? _authorRepository;
        private IGenreRepository? _genreRepository;
        private IShowRepository? _showRepository;
        private ITicketRepository? _ticketRepository;
        
        public override IAuthorRepository Authors => _authorRepository ??= new AuthorRepository(_context);
        public override IGenreRepository Genres => _genreRepository ??= new GenreRepository(_context);
        public override IShowRepository Shows => _showRepository ??= new ShowRepository(_context);
        public override ITicketRepository Tickets => _ticketRepository ??= new TicketRepository(_context);


        public UnitOfWork(PlaybillDbContext context)
        {
            _context = context;
        }

        public override async Task Save()
        {
            await _context.SaveChangesAsync();
        }
        
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}