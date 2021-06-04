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
    class EFDestinationRepositoryTests
    {
        private TripPlannerAppContext _tripPlannerAppContext;
        IDestinationRepository repo;

        [SetUp]
        public void Setup()
        {
            _tripPlannerAppContext = GetInMemoryDBContext();
            _tripPlannerAppContext.Database.EnsureDeleted();
            _tripPlannerAppContext.Database.EnsureCreated();
            repo = new EFDestinationRepository(_tripPlannerAppContext);
        }

        private static Destination MakeDestination() 
        {
            return new Destination
            {
                City="Indio",
                StateProvince="CA",
                Country="USA"
            };
        }

        private static TripPlannerAppContext GetInMemoryDBContext()
        {
            var options = new DbContextOptionsBuilder<TripPlannerAppContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            return new TripPlannerAppContext(options);
        }

        [Test]
        public void ShouldAddDestination()
        {
            var expected = MakeDestination();

            var actual = repo.Add(expected);

            Assert.IsTrue(actual.Success);
            Assert.NotZero(actual.Data.DestinationID);
            Assert.AreEqual(expected.City, actual.Data.City);
        }

        [Test]
        public void ShouldGetDestination()
        {
            var expected = MakeDestination();

            var addResponse = repo.Add(expected);
            var actual = repo.Get(addResponse.Data.DestinationID);

            Assert.IsTrue(actual.Success);
            Assert.IsNotNull(actual.Data);
            Assert.AreEqual(expected.City, actual.Data.City);
        }

        [Test]
        public void ShouldNotGetUnknownDestination()
        {
            var actual = repo.Get(5);

            Assert.IsFalse(actual.Success);
            Assert.IsNull(actual.Data);
        }

        [Test]
        public void ShouldNotAddADestinationIfCityIsNull()
        {
            var data = MakeDestination();
            data.City = null;

            var actual = repo.Add(data);

            Assert.IsFalse(actual.Success);
            Assert.IsNull(actual.Data);
        }

        [Test]
        public void ShouldNotAddADestinationIfCountryIsNull()
        {
            var data = MakeDestination();
            data.Country = null;

            var actual = repo.Add(data);

            Assert.IsFalse(actual.Success);
            Assert.IsNull(actual.Data);
        }

        [Test]
        public void ShouldEditDestination()
        {
            var data = MakeDestination();
            var data2 = MakeDestination();
            data2.City = "Palm Desert";

            var addResponse = repo.Add(data);
            data2.DestinationID = addResponse.Data.DestinationID;
            var editResponse = repo.Edit(data2);
            var getResponse = repo.Get(addResponse.Data.DestinationID);

            Assert.IsTrue(addResponse.Success);
            Assert.IsTrue(editResponse.Success);
            Assert.IsTrue(getResponse.Success);
            Assert.AreEqual(data.City, getResponse.Data.City);
        }

        [Test]
        public void ShouldRemoveDestination() 
        {
            var data = MakeDestination();

            var addResponse = repo.Add(data);
            var removeResponse = repo.Remove(addResponse.Data.DestinationID);
            var getResponse = repo.Get(addResponse.Data.DestinationID);

            Assert.IsTrue(addResponse.Success);
            Assert.IsTrue(removeResponse.Success);
            Assert.IsFalse(getResponse.Success);
        }
    }
}
