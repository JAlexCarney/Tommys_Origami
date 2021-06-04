using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Core;
using TripPlanner.Core.Entities;
using TripPlanner.Core.Interfaces;
using TripPlanner.DAL.Repos;

namespace TripPlanner.DAL.Tests
{
    public class EFTripRepositoryTests
    {
        private TripPlannerAppContext _tripPlannerAppContext;
        ITripRepository tripRepo;
        IUserRepository userRepo;

        [SetUp]
        public void Setup()
        {
            _tripPlannerAppContext = GetInMemoryDBContext();
            _tripPlannerAppContext.Database.EnsureDeleted();
            _tripPlannerAppContext.Database.EnsureCreated();
            tripRepo = new EFTripRepository(_tripPlannerAppContext);
            userRepo = new EFUserRepository(_tripPlannerAppContext);

        }
        public static TripPlannerAppContext GetInMemoryDBContext()
        {
            var options = new DbContextOptionsBuilder<TripPlannerAppContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            return new TripPlannerAppContext(options);
        }

        [Test]
        public void ShouldAddATrip()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsTrue(response.Success);
            Assert.NotZero(trip.TripID);
            Assert.AreEqual(trip, response.Data);
        }

        [Test]
        public void ShouldGetTrip()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;
            tripRepo.Add(trip);

            Response<Trip> response = tripRepo.Get(1);

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
        }

        [Test]
        public void ShouldNotGetTrip()
        {
            Response<Trip> response = tripRepo.Get(5);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }
        /*
        [Test]
        public void ShouldNotAddATripIfUserIdDoesNotExist()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("b1ba3417-41ea-4428-b9dd-8684e6374f8b");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }
        */

        [Test]
        public void ShouldNotAddATripIfStartDateIsInThePast()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("05-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldNotAddATripIfProjectedEndDateIsBeforeStartDate()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-06-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-01-2021");
            trip.IsBooked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfActualEndDateIsPassedIn()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.ActualEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfUserIDIsNotPassedIn()
        {
            Trip trip = new Trip();
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfStartDateIsNotPassedIn()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfProjectedEndDateIsNotPassedIn()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.IsBooked = true;

            Response<Trip> response = tripRepo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldEditTrip()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            tripRepo.Add(trip);

            Trip editTrip = trip;
            editTrip.ProjectedEndDate = DateTime.Parse("08-07-2021");

            Response response = tripRepo.Edit(editTrip);
            Assert.IsTrue(response.Success);

            Response<Trip> result = tripRepo.Get(1);
            Assert.AreEqual(DateTime.Parse("08-07-2021"), result.Data.ProjectedEndDate);
        }

        [Test]
        public void ShouldNotEditIfActualEndDateIsPriorToStartDate()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            tripRepo.Add(trip);

            Trip editTrip = trip;
            editTrip.ActualEndDate = DateTime.Parse("07-29-2021");


            Response response = tripRepo.Edit(editTrip);
            Assert.IsFalse(response.Success);

        }

        [Test]
        public void ShouldRemoveTrip()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            Response<Trip> addResponse = tripRepo.Add(trip);
            Response response = tripRepo.Remove(addResponse.Data.TripID);

            Assert.IsTrue(response.Success);

        }

        [Test]
        public void ShouldNotRemoveTripIfIdDoesNotExist()
        {
            Response response = tripRepo.Remove(3);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void ShouldGetTripsByUserId()
        {
            User user = new User();
            user.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            userRepo.Add(user);

            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.IsBooked = true;

            Response response = tripRepo.Add(trip);

            Trip trip2 = new Trip();
            trip2.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip2.StartDate = DateTime.Parse("10-01-2021");
            trip2.ProjectedEndDate = DateTime.Parse("10-06-2021");
            trip2.IsBooked = false;

            response = tripRepo.Add(trip2);

            Response<List<Trip>> trips = tripRepo.GetByUser(trip.UserID);

            Assert.IsTrue(trips.Success);
            Assert.AreEqual(2, trips.Data.Count);
        }

        [Test]
        public void ShouldNotGetTripByUserIdIfDoesNotExist()
        {
            Response<List<Trip>> trips = tripRepo.GetByUser(Guid.Parse("d1f20446-cf7d-495d-8151-7a3fe68eb153"));
            Assert.IsFalse(trips.Success);
        }

    }
}
