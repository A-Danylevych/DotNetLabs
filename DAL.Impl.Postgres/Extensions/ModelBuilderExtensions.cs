using Microsoft.EntityFrameworkCore;
using Models.Status;
using Status = DAL.Entities.Status;

namespace DAL.Impl.Postgres.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Status>().HasData(new Status
            {
                Id = (int) StatusEnum.Available,
                Name = "Available"
            });
            modelBuilder.Entity<Status>().HasData(new Status
            {
                Id = (int) StatusEnum.Booked,
                Name = "Booked",
            });
            modelBuilder.Entity<Status>().HasData(new Status
            {
                Id = (int) StatusEnum.Sold,
                Name = "Sold",
            });
        }
    }
}