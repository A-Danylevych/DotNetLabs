using DAL.Entities;
using DAL.Impl.Postgres.Extensions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl.Postgres
{
    public sealed class PlaybillDbContext : DbContext
    {
        public PlaybillDbContext(DbContextOptions<PlaybillDbContext> context) : base(context)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(author => author.Id);
            modelBuilder.Entity<Genre>().HasKey(genre => genre.Id);
            modelBuilder.Entity<Show>().HasKey(show => show.Id);
            modelBuilder.Entity<Status>().HasKey(status => status.Id);
            modelBuilder.Entity<Ticket>().HasKey(ticket => ticket.Id);
            
            modelBuilder.Entity<Author>().HasIndex(author => new {author.FirstName, author.LastName}).IsUnique();
            modelBuilder.Entity<Genre>().HasIndex(genre => genre.Name).IsUnique();
            modelBuilder.Entity<Show>().HasIndex(show => new {show.Name, show.Date}).IsUnique();
            modelBuilder.Entity<Ticket>().HasIndex(ticket => new {ticket.Row, ticket.Seat, ticket.ShowId}).IsUnique();

            modelBuilder.Entity<Author>().HasMany(a => a.Shows)
                .WithOne(s => s.Author).HasForeignKey(s => s.AuthorId);
            modelBuilder.Entity<Genre>().HasMany(g => g.Shows)
                .WithOne(s => s.Genre).HasForeignKey(s => s.GenreId);
            modelBuilder.Entity<Show>().HasMany(s => s.Tickets)
                .WithOne(t => t.Show).HasForeignKey(t => t.ShowId);
            modelBuilder.Entity<Status>().HasMany(s => s.Tickets)
                .WithOne(t => t.Status).HasForeignKey(t => t.StatusId);

           
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}