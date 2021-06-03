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
        ITripRepository repo;

        [SetUp]
        public void Setup()
        {
            _tripPlannerAppContext = GetInMemoryDBContext();
            _tripPlannerAppContext.Database.EnsureDeleted();
            _tripPlannerAppContext.Database.EnsureCreated();
            repo = new EFTripRepository(_tripPlannerAppContext);
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
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsTrue(response.Success);
            Assert.NotZero(trip.TripID);
            Assert.AreEqual(trip, response.Data);
        }

        [Test]
        public void ShouldGetTrip()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;
            repo.Add(trip);

            Response<Trip> response = repo.Get(1);

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
        }

        [Test]
        public void ShouldNotGetTrip()
        {
            Response<Trip> response = repo.Get(5);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }
        [Test]
        public void ShouldNotAddATripIfUserIDDoesNotExist()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("b1ba3417-41ea-4428-b9dd-8684e6374f8b");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldNotAddATripIfStartDateIsInThePast()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("b1ba3417-41ea-4428-b9dd-8684e6374f8b");
            trip.StartDate = DateTime.Parse("05-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldNotAddATripIfProjectedEndDateIsBeforeStartDate()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-06-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-01-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfActualEndDateIsPassedIn()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.ActualEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfUserIDIsNotPassedIn()
        {
            Trip trip = new Trip();
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfStartDateIsNotPassedIn()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldFailIfProjectedEndDateIsNotPassedIn()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.Booked = true;

            Response<Trip> response = repo.Add(trip);

            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Data);
        }

        [Test]
        public void ShouldEditTrip()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            repo.Add(trip);

            Trip editTrip = trip;
            editTrip.ProjectedEndDate = DateTime.Parse("08-07-2021");

            Response response = repo.Edit(editTrip);
            Assert.IsTrue(response.Success);

            Response<Trip> result = repo.Get(1);
            Assert.AreEqual(result.Data.ProjectedEndDate, DateTime.Parse("08-07-2021"));
        }

        [Test]
        public void ShouldNotEditIfActualEndDateIsPriorToStartDate()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            repo.Add(trip);

            Trip editTrip = trip;
            editTrip.ActualEndDate = DateTime.Parse("07-29-2021");


            Response response = repo.Edit(editTrip);
            Assert.IsFalse(response.Success);

        }

        [Test]
        public void ShouldRemoveTrip()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response response = repo.Remove(1);

            Assert.IsTrue(response.Success);

        }

        [Test]
        public void ShouldNotRemoveTripIfIdDoesNotExist()
        {
            Response response = repo.Remove(3);
            Assert.IsFalse(response.Success);
        }

        [Test]
        public void ShouldGetTripsByUserId()
        {
            Trip trip = new Trip();
            trip.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip.StartDate = DateTime.Parse("08-01-2021");
            trip.ProjectedEndDate = DateTime.Parse("08-06-2021");
            trip.Booked = true;

            Response response = repo.Add(trip);

            Trip trip2 = new Trip();
            trip2.UserID = Guid.Parse("25f160d5-d3c6-4944-b3e5-a8d6d29831c8");
            trip2.StartDate = DateTime.Parse("10-01-2021");
            trip2.ProjectedEndDate = DateTime.Parse("10-06-2021");
            trip2.Booked = false;

            response = repo.Add(trip2);

            Response<List<Trip>> trips = repo.GetByUser(trip.UserID);

            Assert.IsTrue(trips.Success);
            Assert.AreEqual(trips.Data.Count, 2);
        }

        [Test]
        public void ShouldNotGetTripByUserIdIfTrip()
        {
            Response<List<Trip>> trips = repo.GetByUser(Guid.Parse("d1f20446-cf7d-495d-8151-7a3fe68eb153"));
            Assert.IsFalse(trips.Success);
        }

    }
}
