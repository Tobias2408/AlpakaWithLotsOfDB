using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestAlpaka.Model;

namespace RestAlpaka.Model
{
    public class AlpakaDbContext : DbContext
    {
        public AlpakaDbContext(DbContextOptions<AlpakaDbContext> options) : base(options)
        {
        }

        public DbSet<Alpaka> Alpakas { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Location> Location { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Users> Users { get; set; }
       
    }
}

