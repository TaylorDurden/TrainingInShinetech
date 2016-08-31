using System.Collections.Generic;
using System.Linq;
using AutoMoq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebAPI.Tests.Controller
{
    [TestFixture]
    public class UserControllerTests
    {
        private AutoMoqer _mocker;
        private UserController _userController;

        [SetUp]
        public void Initialize()
        {
            _mocker = new AutoMoqer();
            _userController = _mocker.Create<UserController>();
        }

        [TearDown]
        public void Dispose()
        {
            _mocker = null;
            _userController.Dispose();
        }

        
        [Test]
        public void Create_when_userLogicModel_is_null_should_never_execute_and_return_is_null()
        {
            // Arrange
            var userLogic = _mocker.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Create(null));
            // Act
            var result = _userController.Create(null);
            // Assert
            userLogic.Verify(x => x.Create(null), Times.Never());
            Assert.IsNull(result);
        }

        [Test]
        public void Create_when_userLogicModel_is_not_null_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123456",
                Email = "zhourui@shinetechchina.com",
                Image = "",
                Message = ""
            };
            var userLogic = _mocker.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Create(It.IsAny<UserLogicModel>())).Returns(userLogicModel);
            // Act
            var result = _userController.Create(userLogicModel);
            // Assert
            userLogic.Verify(x => x.Create(userLogicModel), Times.Once());
            Assert.AreEqual(typeof(UserViewModel),result.GetType());
            Assert.AreEqual(userLogicModel.UserId,result.UserId);
            Assert.AreEqual(userLogicModel.UserName, result.UserName);
            Assert.AreEqual(userLogicModel.Password, result.Password);
            Assert.AreEqual(userLogicModel.Email, result.Email);
            Assert.AreEqual(userLogicModel.Image, result.Image);
            Assert.AreEqual(userLogicModel.Message, result.Message);
        }
        

        
        [Test]
        public void Delete_should_execute_once()
        {
            // Arrange
            var userLogic = _mocker.GetMock<IUserLogic>();
            // Act
            _userController.Delete(It.IsAny<int>());
            // Assert
            userLogic.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }
        

        

        [Test]
        public void Edit_when_userLogicModel_not_null_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123456",
                Email = "zhourui@shinetechchina.com",
                Image = "",
                Message = ""
            };
            var userLogic = _mocker.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Edit(userLogicModel)).Returns(userLogicModel);
            // Act
            var result = _userController.Edit(userLogicModel);
            // Assert
            userLogic.Verify(x => x.Edit(userLogicModel), Times.Once());
            Assert.AreEqual(typeof (UserViewModel), result.GetType());
            Assert.AreEqual(userLogicModel.UserId, result.UserId);
            Assert.AreEqual(userLogicModel.UserName, result.UserName);
            Assert.AreEqual(userLogicModel.Password, result.Password);
            Assert.AreEqual(userLogicModel.Email, result.Email);
            Assert.AreEqual(userLogicModel.Image, result.Image);
            Assert.AreEqual(userLogicModel.Message, result.Message);
        }

        [Test]
        public void Edit_when_userLogicModel_is_null_should_return_null()
        {
            // Arrange
            var userLogic = _mocker.GetMock<IUserLogic>();
            // Act
            var result = _userController.Edit(null);
            // Assert
            userLogic.Verify(x => x.Edit(null), Times.Once());
            Assert.IsNull(result);
        }
        

        
        [Test]
        public void Login_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var userLogicModel = new UserLogicModel
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123456",
                Email = "zhourui@shinetechchina.com",
                Image = "",
                Message = ""
            };
            var userLogic = _mocker.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(userLogicModel);
            // Act
            var result = _userController.Login(It.IsAny<string>(), It.IsAny<string>());
            // Assert
            userLogic.Verify(x => x.Login(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.AreEqual(typeof(UserViewModel),result.GetType());
            Assert.AreEqual(userLogicModel.UserId, result.UserId);
            Assert.AreEqual(userLogicModel.UserName, result.UserName);
            Assert.AreEqual(userLogicModel.Password, result.Password);
            Assert.AreEqual(userLogicModel.Email, result.Email);
            Assert.AreEqual(userLogicModel.Image, result.Image);
            Assert.AreEqual(userLogicModel.Message, result.Message);
        }
        

        
        [Test]
        public void GetAllUsers_should_execute_once_and_return_users_count()
        {
            // Arrange
            var users = new List<UserLogicModel>
            {
                new UserLogicModel()
                {
                    UserId = 1,
                    UserName = "zhourui",
                    Password = "123123",
                    Email = "zhourui@shinetechchina.com",
                    Image = ""
                }
            };
            var userLogic = _mocker.GetMock<IUserLogic>();
            userLogic.Setup(x => x.GetAll()).Returns(users);
            // Act
            var result = _userController.GetAllUsers();

            // Assert
            userLogic.Verify(x => x.GetAll(), Times.Once());
            result.Count().Should().Be(users.Count);
        }

        [Test]
        public void GetUserById_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var userLogicModel = new UserLogicModel()
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123123",
                Email = "zhourui@shinetechchina.com",
                Image = ""
            };
            var userLogic = _mocker.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Get(It.IsAny<int>())).Returns(userLogicModel);
            // Act
            var result = _userController.GetUserById(It.IsAny<int>());
            // Assert
            userLogic.Verify(x => x.Get(It.IsAny<int>()), Times.Once());
            Assert.AreEqual(typeof(UserViewModel),result.GetType());
            Assert.AreEqual(userLogicModel.UserId, result.UserId);
            Assert.AreEqual(userLogicModel.UserName, result.UserName);
            Assert.AreEqual(userLogicModel.Password, result.Password);
            Assert.AreEqual(userLogicModel.Email, result.Email);
            Assert.AreEqual(userLogicModel.Image, result.Image);
            Assert.AreEqual(userLogicModel.Message, result.Message);
        }

        [Test]
        public void GetUserByName_should_execute_once_and_return_right_type_and_right_values()
        {
            // Arrange
            var userLogicModel = new UserLogicModel()
            {
                UserId = 1,
                UserName = "zhourui",
                Password = "123123",
                Email = "zhourui@shinetechchina.com",
                Image = ""
            };
            var userLogic = _mocker.GetMock<IUserLogic>();
            userLogic.Setup(x => x.Get(It.IsAny<string>())).Returns(userLogicModel);
            // Act
            var result = _userController.GetUserByName(It.IsAny<string>());
            // Assert
            userLogic.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(typeof(UserViewModel),result.GetType());
            Assert.AreEqual(userLogicModel.UserId, result.UserId);
            Assert.AreEqual(userLogicModel.UserName, result.UserName);
            Assert.AreEqual(userLogicModel.Password, result.Password);
            Assert.AreEqual(userLogicModel.Email, result.Email);
            Assert.AreEqual(userLogicModel.Image, result.Image);
            Assert.AreEqual(userLogicModel.Message, result.Message);
        }
        
    }
}
