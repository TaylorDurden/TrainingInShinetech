using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using FluentAssertions;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Controllers;
using Assert = NUnit.Framework.Assert;

namespace PlanPoker.Test.Web
{
    [TestFixture]
    public class ControllerTest
    {
        private AutoMoqer _autoMoqer;
        private UserController _userController;

        [SetUp]
        public void SetupUserController()
        {
            _autoMoqer = new AutoMoqer();
            _userController = _autoMoqer.Create<UserController>();
        }

        [Test]
        public void TestUserControllerGetAll()
        {
            var user = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123456",
                    Email = "zhourui@shinetechchina.com",
                    Image = ""
                },
                new User
                {
                    UserId = 2,
                    UserName = "zhang3",
                    Password = "123456",
                    Email = "zhang3@shinetechchina.com",
                    Image = ""
                }
            };
            _autoMoqer.GetMock<IUserLogic>().Setup(x => x.GetAll()).Returns(user);
            var result = _userController.GetAll();
            result.Count().Should().Be(2);
        }

        [Test]
        public void TestUserControllerGetById()
        {
            var user = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123456",
                    Email = "zhourui@shinetechchina.com",
                    Image = ""
                },
                new User
                {
                    UserId = 2,
                    UserName = "zhang3",
                    Password = "123456",
                    Email = "zhang3@shinetechchina.com",
                    Image = ""
                }
            };
            _autoMoqer.GetMock<IUserLogic>().Setup(x => x.Get(2)).Returns(user[1]);
            var result = _userController.GetById(2);
            var assertUser = new User
            {
                UserId = 2,
                UserName = "zhang3",
                Password = "123456",
                Email = "zhang3@shinetechchina.com",
                Image = ""
            };
            Assert.AreEqual(result, assertUser);
        }

        [Test]
        public void TestUserControllerGetByName()
        {
            var user = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123456",
                    Email = "zhourui@shinetechchina.com",
                    Image = ""
                },
                new User
                {
                    UserId = 2,
                    UserName = "zhang3",
                    Password = "123456",
                    Email = "zhang3@shinetechchina.com",
                    Image = ""
                }
            };
            _autoMoqer.GetMock<IUserLogic>().Setup(x => x.Get(1)).Returns(user[0]);
            var result = _userController.GetByName("zhourui");
            var assertUser = new User
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123456",
                Email = "zhourui@shinetechchina.com",
                Image = ""
            };
            Assert.AreEqual(result, assertUser);
        }

        [Test]
        public void TestUserControllerLogin()
        {
            var user = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123456",
                    Email = "zhourui@shinetechchina.com",
                    Image = ""
                },
                new User
                {
                    UserId = 2,
                    UserName = "zhang3",
                    Password = "123456",
                    Email = "zhang3@shinetechchina.com",
                    Image = ""
                }
            };
            _autoMoqer.GetMock<IUserLogic>().Setup(x => x.Login("zhourui", "123456")).Returns("");
            var result = _userController.GetById(2);
            var assertUser = new User
            {
                UserId = 2,
                UserName = "zhang3",
                Password = "123456",
                Email = "zhang3@shinetechchina.com",
                Image = ""
            };
            Assert.AreEqual(result, assertUser);
        }

        [TearDown]
        public void TearDownUserController()
        {
            _autoMoqer = null;
            _userController.Dispose();
        }
    }
}
