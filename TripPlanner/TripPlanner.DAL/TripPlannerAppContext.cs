using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core.Entities;

namespace TripPlanner.DAL
{
    public class TripPlannerAppContext : DbContext
    {
        public DbSet<Destination> Destination { get; set; }
        public DbSet<DestinationTrip> DestinationTrip { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<User> User { get; set; }

        public TripPlannerAppContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DestinationTrip>().HasKey(aa => new { aa.DestinationID, aa.TripID });
            modelBuilder.Entity<Review>().HasKey(aa => new { aa.DestinationID, aa.UserID });
            modelBuilder.Entity<User>().HasMany(u => u.Trips).WithOne(t => t.User);
        }

        public static TripPlannerAppContext GetDBContext()
        {
            var options = new DbContextOptionsBuilder<TripPlannerAppContext>()
                .UseSqlServer(SettingsManager.GetConnectionString())
                .Options;
            return new TripPlannerAppContext(options);
        }
    }
}
