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
        public DbSet<DestinationTrip> TripDestination { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<User> User { get; set; }

        public TripPlannerAppContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Destination>().HasMany(d => d.Trips).WithMany(t => t.Destinations)
            //    .UsingEntity<Dictionary<string, object>>
            //    (
            //        "DestinationTrip",
            //        dt => dt.HasOne<Trip>().WithMany().HasForeignKey("TripID"),
            //        dt => dt.HasOne<Destination>().WithMany().HasForeignKey("DestinationID")
            //    );
            modelBuilder.Entity<DestinationTrip>().HasKey(aa => new { aa.DestinationID, aa.TripID });
            modelBuilder.Entity<Review>().HasKey(aa => new { aa.DestinationID, aa.UserID });
            modelBuilder.Entity<User>().HasMany(u => u.Trips).WithOne(t => t.User);
            /*
            modelBuilder.Entity<Destination>().HasMany<Review>();
            modelBuilder.Entity<DestinationTrip>().HasOne<Destination>(); //needed?
            modelBuilder.Entity<DestinationTrip>().HasOne<Trip>(); //needed?
            modelBuilder.Entity<Review>().HasOne<Destination>();
            modelBuilder.Entity<Review>().HasOne<User>();
            modelBuilder.Entity<Trip>().HasMany<DestinationTrip>();
            modelBuilder.Entity<Trip>().HasOne<User>();
            modelBuilder.Entity<User>().HasMany<Review>();
            modelBuilder.Entity<User>().HasMany<Trip>();
            */
        }

        public static TripPlannerAppContext GetDBContext()
        {
            var options = new DbContextOptionsBuilder<TripPlannerAppContext>()
                .UseSqlServer("Server=localhost;Database=TripPlanner;User Id=sa;Password=YOUR_strong_*pass4w0rd*")
                .Options;
            return new TripPlannerAppContext(options);
        }
    }
}
