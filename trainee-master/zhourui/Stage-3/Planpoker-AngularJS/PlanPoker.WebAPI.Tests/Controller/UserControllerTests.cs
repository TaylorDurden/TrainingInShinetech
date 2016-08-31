using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Web.Http;
using AutoMoq;
using FluentAssertions;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic;
using PlanPoker.WebAPI.Controllers;

namespace PlanPoker.WebAPI.Tests.Controller
{
    [TestFixture]
    public class RepositoryBaseTests
    {
        private AutoMoqer _autoMoqer;
        private UserController _userController;

        [SetUp]
        public void SetUp()
        {
            _autoMoqer = new AutoMoqer();
            _userController = _autoMoqer.Create<UserController>();
        }

        [Test]
        public void Create()
        {
            //var message = _userLogic.Create(user);
            //HttpResponseMessage response;
            //if (string.IsNullOrEmpty(message))
            //{
            //    response = Request.CreateResponse(HttpStatusCode.Created, user.UserName);
            //}
            //else
            //{
            //    response = Request.CreateResponse(HttpStatusCode.BadRequest);
            //    response.Content = new StringContent(message);
            //}
            //return response;

            // Arrange
            //_userController.Request = new HttpRequestMessage();
            //_userController.Request.SetConfiguration(new HttpConfiguration());
            // Act

            //var result = _userController.Create();

            // Assert

        }

        [Test]
        public void GetAll_should_return_1()
        {
            // Arrange
            var users = new List<User>
            {
                new User()
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123123",
                    Email = "zhourui@shinetechchina.com",
                    Image = ""
                }
            };
            var userLogic = _autoMoqer.GetMock<IUserLogic>();
            userLogic.Setup(x => x.GetAll()).Returns(users);
            // Act
            var result = _userController.GetAll();

            // Assert
            result.Count().Should().Be(1);
        }

        [Test]
        public void GetById_should_equals_user()
        {
            // Arrange
            var user = new User()
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123123",
                Email = "zhourui@shinetechchina.com",
                Image = ""
            };
            var userLogic = _autoMoqer.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Get(1)).Returns(user);
            // Act
            var result = _userController.GetById(1);
            // Assert
            result.Equals(user);
        }

        [Test]
        public void GetByName_should_equals_user()
        {
            // Arrange
            var user = new User()
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123123",
                Email = "zhourui@shinetechchina.com",
                Image = ""
            };
            var userLogic = _autoMoqer.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Get("zhourui")).Returns(user);
            // Act
            var result = _userController.GetByName("zhourui");
            // Assert
            result.Equals(user);
        }
    }
}
