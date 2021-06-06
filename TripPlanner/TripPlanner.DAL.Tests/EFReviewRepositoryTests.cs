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
    public class EFReviewRepositoryTests
    {
        private TripPlannerAppContext _tripPlannerAppContext;
        IReviewRepository repo;

        [SetUp]
        public void Setup()
        {
            _tripPlannerAppContext = GetInMemoryDBContext();
            _tripPlannerAppContext.Database.EnsureDeleted();
            _tripPlannerAppContext.Database.EnsureCreated();
            repo = new EFReviewRepository(_tripPlannerAppContext);
        }

        private static Review MakeReview()
        {
            return new Review
            {
                DestinationID = 1,
                UserID = Guid.Parse("2ac1a40d-a606-48e9-a807-09cfa0facc0a"),
                Description = "Lorem Ipsem",
                Rating = 4.3M
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
        public void ShouldAddReview()
        {
            Review review = MakeReview();

            Response<Review> result = repo.Add(review);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(review.Rating, result.Data.Rating);
        }

        [Test]
        public void ShouldNotAddIfDescriptionIsNull()
        {
            Review review = MakeReview();
            review.Description = null;
            Response<Review> result = repo.Add(review);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
        }

        [Test]
        public void ShouldNotAddIfRatingIsAboveFive()
        {
            Review review = MakeReview();
            review.Rating = 6.0M;
            Response<Review> result = repo.Add(review);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
        }

        [Test]
        public void ShouldNotAddReviewIfRatingIsBelowZero()
        {
            Review review = MakeReview();
            review.Rating = -1.0M;
            Response<Review> result = repo.Add(review);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
        }

        [Test]
        public void ShouldGetReview()
        {
            Review review = MakeReview();

            repo.Add(review);

            Response<Review> result = repo.Get(review.DestinationID, review.UserID);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(review.Rating, result.Data.Rating);
        }

        [Test]
        public void ShouldNotGetIfDestinationIdDoesNotExist()
        {
            Review review = MakeReview();

            repo.Add(review);
            var actual = repo.Get(6,review.UserID);

            Assert.IsFalse(actual.Success);
            Assert.IsNull(actual.Data);
        }

        [Test]
        public void ShouldNotGetIfUserIdDoesNotExist()
        {
            Review review = MakeReview();

            repo.Add(review);
            var actual = repo.Get(review.DestinationID, Guid.Parse("cb1840d7-2fda-4420-b133-2b687394afa3"));

            Assert.IsFalse(actual.Success);
            Assert.IsNull(actual.Data);
        }

        [Test]
        public void ShouldGetReviewsByDestination()
        {
            Review review = MakeReview();

            repo.Add(review);
            Review newReview = new Review
            {
                DestinationID = 1,
                UserID = Guid.Parse("cb1840d7-2fda-4420-b133-2b687394afa3"),
                Description = "Lorem Ipsem",
                Rating = 4.3M
            };
            repo.Add(newReview);
            Response<List<Review>> result = repo.GetByDestination(review.DestinationID);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
        }

        /*
        [Test]
        public void ShouldNotGetReviewsByDestination()
        {
            Response<List<Review>> result = repo.GetByDestination(3);

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
        }
        */

        [Test]
        public void ShouldGetReviewsByUser()
        {
            Review review = MakeReview();

            repo.Add(review);
            Review newReview = new Review
            {
                DestinationID = 3,
                UserID = Guid.Parse("2ac1a40d-a606-48e9-a807-09cfa0facc0a"),
                Description = "Lorem Ipsem",
                Rating = 4.3M
            };
            repo.Add(newReview);
            Response<List<Review>> result = repo.GetByUser(review.UserID);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(2, result.Data.Count);
        }

        /*
        [Test]
        public void ShouldNotGetReviewsByUser()
        {
            Response<List<Review>> result = repo.GetByUser(Guid.Parse("2ac1a40d-a606-48e9-a807-09cfa0facc0a"));

            Assert.IsFalse(result.Success);
            Assert.IsNull(result.Data);
        }
        */

        [Test]
        public void ShouldEditReview()
        {
            Review review = MakeReview();

            repo.Add(review);
            Review editReview = MakeReview();
            editReview.Rating = 3.3M;

            Response result = repo.Edit(editReview);
            Response<Review> response = repo.Get(review.DestinationID, review.UserID);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(editReview.Rating, response.Data.Rating);
        }

        [Test]
        public void ShouldNotEditReviewIfDescriptionIsNull()
        {
            Review review = MakeReview();

            repo.Add(review);
            Review editReview = MakeReview();
            editReview.Description = null;

            Response result = repo.Edit(editReview);

            Assert.IsFalse(result.Success);
        }

        [Test]
        public void ShouldRemoveReview()
        {
            Review review = MakeReview();

            Response<Review> addResponse = repo.Add(review);
            Response remove = repo.Remove(review.DestinationID, review.UserID);
            var getResponse = repo.Get(review.DestinationID, review.UserID);

            Assert.IsTrue(addResponse.Success);
            Assert.IsTrue(remove.Success);
            Assert.IsFalse(getResponse.Success);
        }

        [Test]
        public void ShouldNotRemoveReview()
        {

            Response remove = repo.Remove(7, Guid.Parse("2ac1a40d-a606-48e9-a807-09cfa0facc0a"));

            Assert.IsFalse(remove.Success);
        }
    }
}
