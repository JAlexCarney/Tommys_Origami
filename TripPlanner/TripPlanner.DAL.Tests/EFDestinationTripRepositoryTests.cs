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
    public class EFDestinationTripRepositoryTests
    {

        private TripPlannerAppContext _tripPlannerAppContext;
        IDestinationTripRepository repo;

        [SetUp]
        public void Setup()
        {
            _tripPlannerAppContext = GetInMemoryDBContext();
            _tripPlannerAppContext.Database.EnsureDeleted();
            _tripPlannerAppContext.Database.EnsureCreated();
            repo = new EFDestinationTripRepository(_tripPlannerAppContext);
        }

        private static TripPlannerAppContext GetInMemoryDBContext()
        {
            var options = new DbContextOptionsBuilder<TripPlannerAppContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            return new TripPlannerAppContext(options);
        }

        private static DestinationTrip MakeDestinationTrip()
        {
            DestinationTrip dt = new DestinationTrip();
            dt.DestinationID = 1;
            dt.TripID = 1;
            dt.Description = "description";

            return dt;
        }

        //add, edit, get, get by trip, remove

        [Test]
        public void ShouldAddDestinationTrip()
        {
            DestinationTrip expected = MakeDestinationTrip();

            Response<DestinationTrip> response = repo.Add(expected);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(1, response.Data.DestinationID);
            Assert.AreEqual(1, response.Data.TripID);
            Assert.AreEqual("description", response.Data.Description);
        }

        //should not add w/o tests - w/o destinationid/tripid
        [Test]
        public void ShouldNotAddDestinationTripWODestinationID()
        {
            DestinationTrip expected = MakeDestinationTrip();
            expected.DestinationID = 0;

            Response<DestinationTrip> response = repo.Add(expected);

            Assert.IsFalse(response.Success);
            Assert.Null(response.Data);
            Assert.AreEqual("DestinationID is required", response.Message); //check exact message
        }

        [Test]
        public void ShouldNotAddDestinationTripWOTripID()
        {
            DestinationTrip expected = MakeDestinationTrip();
            expected.TripID = 0;

            Response<DestinationTrip> response = repo.Add(expected);

            Assert.IsFalse(response.Success);
            Assert.Null(response.Data);
            Assert.AreEqual("TripID is required", response.Message); //check exact message
        }

        [Test]
        public void ShouldEditDestinationTrip()
        {
            DestinationTrip expected = MakeDestinationTrip();
            repo.Add(expected);

            DestinationTrip updated = expected;
            updated.Description = "updated";

            Response response = repo.Edit(updated);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("updated", expected.Description);
        }

        //able to edit destination ids/tripids ? tests needed ?

        [Test]
        public void ShouldNotEditDestinationTripDestinationID()
        {
            DestinationTrip expected = MakeDestinationTrip();
            repo.Add(expected);

            DestinationTrip updated = expected;
            updated.DestinationID = 2;

            Response response = repo.Edit(updated);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Cannot edit DestinationID", response.Message);
        }

        [Test]
        public void ShouldGetDestinationTrip()
        {
            DestinationTrip expected = MakeDestinationTrip();
            Response<DestinationTrip> addedResponse = repo.Add(expected);

            Response<DestinationTrip> response = repo.Get(expected.DestinationID, expected.TripID);

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(expected, response.Data);
        }

        [Test]
        public void ShouldNotGetDestinationTrip()
        {
            DestinationTrip expected = MakeDestinationTrip();

            Response<DestinationTrip> response = repo.Get(expected.DestinationID, expected.TripID);

            Assert.IsFalse(response.Success);
            Assert.Null(response.Data);
        }

        [Test]
        public void ShouldGetDestinationTripByTrip()
        {
            DestinationTrip expected = MakeDestinationTrip();
            Response<DestinationTrip> addedResponse = repo.Add(expected);

            Response<List<DestinationTrip>> response = repo.GetByTrip(addedResponse.Data.TripID);

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
            Assert.AreEqual(1, response.Data.Count);
            Assert.AreEqual(expected, response.Data[0]);
        }

        [Test]
        public void ShouldRemoveDestinationTrip()
        {
            DestinationTrip expected = MakeDestinationTrip();
            Response<DestinationTrip> addedResponse = repo.Add(expected);

            Response response = repo.Remove(expected.DestinationID, expected.TripID);

            Response<DestinationTrip> getResponse = repo.Get(expected.DestinationID, expected.TripID);

            Assert.IsTrue(response.Success);
            Assert.IsFalse(getResponse.Success);
            Assert.Null(getResponse.Data);
        }
    }
}
