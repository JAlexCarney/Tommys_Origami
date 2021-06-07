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
    class ADOReportsRepositoryTests
    {
        private ADOReportsRepository repo;

        [OneTimeSetUp]
        public void Setup()
        {
            string connectionString = "Server=localhost;Database=TripPlanner;User Id=sa;Password=YOUR_strong_*pass4w0rd*";
            //repo = new ADOReportsRepository(connectionString);
        }

        [Test]
        public void ShouldReportTopRatedDestinations() 
        {
            var response = repo.GetTopRatedDestintions();

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.Count <= 10);
        }

        [Test]
        public void ShouldReportMostVisitedDestinations() 
        {
            var response = repo.GetMostVisitedDestintions();

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.Count <= 10);
        }

        [Test]
        public void ShouldReportMostRatedDestinations() 
        {
            var response = repo.GetMostRatedDestintions();

            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Data);
            Assert.IsTrue(response.Data.Count <= 10);
        }
    }
}
