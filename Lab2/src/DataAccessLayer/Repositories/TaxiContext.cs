using Microsoft.EntityFrameworkCore;
using Taxi.DAL.Models;

namespace TaxiDAL.Repositories
{
    public class TaxiContext : DbContext
    {
        public TaxiContext(DbContextOptions<TaxiContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CarDto> Cars { get; set; }

        public DbSet<ClientDto> Clients { get; set; }

        public DbSet<DriverDto> Drivers { get; set; }

        public DbSet<OrderDto> Orders { get; set; }
    }
}