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
    public class EFUserRepositoryTests
    {
        private TripPlannerAppContext _tripPlannerAppContext;
        IUserRepository repo;

        [SetUp]
        public void Setup()
        {
            _tripPlannerAppContext = GetInMemoryDBContext();
            _tripPlannerAppContext.Database.EnsureDeleted();
            _tripPlannerAppContext.Database.EnsureCreated();
            repo = new EFUserRepository(_tripPlannerAppContext);
        }

        public static TripPlannerAppContext GetInMemoryDBContext()
        {
            var options = new DbContextOptionsBuilder<TripPlannerAppContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;
            return new TripPlannerAppContext(options);
        }

        //test add
        [Test]
        public void ShouldAddUser()
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");

            Response<User> response = repo.Add(user);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(user, response.Data);
        }

        [Test]
        public void ShouldNotAddUserWOPassword()
        {
            User user = new User();
            //user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");

            Response<User> response = repo.Add(user);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Password is required", response.Message);
            
        }

        [Test]
        public void ShouldNotAddUserWOEmail()
        {
            User user = new User();
            user.Password = "password";
            //user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");

            Response<User> response = repo.Add(user);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Email is required", response.Message);

        }

        [Test]
        public void ShouldNotAddUserWODateCreated()
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            //user.DateCreated = DateTime.Parse("06-01-2021");

            Response<User> response = repo.Add(user);

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Date created is required", response.Message);

        }

        //test edit
        [Test]
        public void ShouldEditDateCreated()
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            repo.Add(user);

            User userUp = user;
            userUp.DateCreated = DateTime.Parse("05-01-2021");

            Response response = repo.Edit(userUp);

            Assert.IsTrue(response.Success);
            Assert.AreEqual(user.DateCreated, DateTime.Parse("05-01-2021"));
        }

        [Test]
        public void ShouldEditEmail()
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            repo.Add(user);

            User userUp = user;
            userUp.Email = "userUp@user.com";

            Response response = repo.Edit(userUp);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("userUp@user.com", user.Email);
        }

        [Test]
        public void ShouldEditPassword()
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            repo.Add(user);

            User userUp = user;
            userUp.Password = "passwordUp";

            Response response = repo.Edit(userUp);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("passwordUp", user.Password);
        }

        [Test]
        public void ShouldEditUsername()
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            user.Username = "user";
            repo.Add(user);

            User userUp = user;
            userUp.Username = "userUp";

            Response response = repo.Edit(userUp);

            Assert.IsTrue(response.Success);
            Assert.AreEqual("userUp", user.Username);
        }

        //test delete
        [Test]
        public void ShouldRemoveUser() 
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            repo.Add(user);

            Response response = repo.Remove(user.UserID);

            Assert.IsTrue(response.Success);
        }

        //test get
        [Test]
        public void ShouldGetUser()
        {
            User user = new User();
            user.Password = "password";
            user.Email = "user@user.com";
            user.DateCreated = DateTime.Parse("06-01-2021");
            repo.Add(user);

            Response response = repo.Get(user.UserID);

            Assert.IsTrue(response.Success);
            //Assert.AreEqual(user, response.Data);
        }

        //test get all
        [Test]
        public void ShouldGetAllUsers()
        {
            User user1 = new User();
            user1.Password = "password1";
            user1.Email = "user1@user.com";
            user1.DateCreated = DateTime.Parse("06-01-2021");
            repo.Add(user1);

            User user2 = new User();
            user2.Password = "password2";
            user2.Email = "user2@user.com";
            user2.DateCreated = DateTime.Parse("06-04-2021");
            repo.Add(user2);

            Response<List<User>> response = repo.GetAll();

            Assert.IsTrue(response.Success);
            Assert.AreEqual(2, response.Data.Count);
            Assert.AreEqual(user1, response.Data[0]);
            Assert.AreEqual(user2, response.Data[1]);
        }

        [Test]
        public void ShouldNotGetAllUsers()
        {
            Response<List<User>> response = repo.GetAll();

            Assert.IsFalse(response.Success);
            Assert.AreEqual("Could not find any users", response.Message);
        }

    }
}

/*


[Test]
*/