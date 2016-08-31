using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.ILogic;
using PlanPoker.ILogic.Models;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebAPI.Tests
{
    [TestFixture]
    public class UserControllerTest 
    {
        private Mock<IUserLogic> _userLogicMock;
        private UserController _userController;
        private UserViewModel _userViewModel;
        private UserLogicModel _userLogicModel;

        [SetUp]
        public void Init()
        {
            _userLogicMock = new Mock<IUserLogic>();
            _userController = new UserController(_userLogicMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _userLogicMock = null;
            _userController = null;
        }

        [Test]
        public void Create_should_execute_and_return_a_string_when_Create_an_user()
        {
            // Arrange
            _userViewModel = new UserViewModel()
            {
                UserName = "lee",
                Password = "123",
                Email = "123@qq.com"
            };
            // Act
            _userLogicMock.Setup(u => u.Create(It.IsAny<UserLogicModel>()));
            var actual = _userController.Create(_userViewModel);
            // Assert
            _userLogicMock.Verify(u => u.Create(It.IsAny<UserLogicModel>()), Times.AtLeastOnce);
            Assert.AreEqual(It.IsAny<string>(), actual);
        }

        [Test]
        public void Delete_should_execute_once_When_there_is_a_user()
        {
            //Arrange
            int id = 10;
            _userLogicMock.Setup(x => x.Delete(id));
            //Act
            _userController.Delete(id);
            //Assert
            _userLogicMock.Verify(v=>v.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Edit_should_execute_once()
        {
            //Arrange
            _userViewModel = new UserViewModel()
            {
                UserId = "10",
                UserName = "lee",
                Password = "123",
                Email = "123@qq.com"
            };

            _userLogicMock.Setup(u => u.Edit(It.IsAny<UserLogicModel>()));

            //Act
            _userController.Edit(_userViewModel);

            //Assert
            _userLogicMock.Verify(v => v.Edit(It.IsAny<UserLogicModel>()), Times.AtLeastOnce);
            Assert.AreEqual(_userViewModel.UserName, "lee");
        }

        [Test]
        public void GetAll_should_return_the_same_count_of_returns()
        {
            //Arrange
            _userLogicModel = new UserLogicModel()
            {
                UserName = "lee",
                Password = "123",
                Email = "123@qq.com"
            };
            List<UserLogicModel> userLogicModels = new List<UserLogicModel> { _userLogicModel };
            _userLogicMock.Setup(u => u.GetAll()).Returns(userLogicModels).Verifiable();
            //Act
            var actual = _userController.GetAll();
            //Assert
            Assert.AreEqual(userLogicModels.Count(), actual.Count());
        }

        [Test]
        public void Login_should_return_a_string_which_is_not_null()
        {
            //Arrange
            string userName = "lee";
            string password = "123";
            string expectedString = "1";

            _userLogicMock.Setup(u => u.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(expectedString).Verifiable();
            //Act
            var actual = _userController.Login(userName, password);
            //Assert
            Assert.AreEqual(expectedString, actual);
        }

        [Test]
        public void QueryByName()
        {
            //Arrange
            string userName = "lee";
            _userLogicModel = new UserLogicModel()
            {
                UserName = "lee",
                Password = "123",
                Email = "123@qq.com"
            };
            List<UserLogicModel> userLogicModels = new List<UserLogicModel> { _userLogicModel };

            _userLogicMock.Setup(u => u.QueryByName(It.IsAny<string>())).Returns(userLogicModels).Verifiable();
            //Act
            var actual = _userController.QueryByName(userName);
            //Assert
            Assert.AreEqual(userLogicModels.Count(), actual.Count());
        }

        [Test]
        public void CheckUserByName_should_return_false_when_there_is_no_user_name()
        {
            //Arrange
            string userName = "lee";
            _userLogicModel = new UserLogicModel()
            {
                UserName = "lee123",
                Password = "123",
                Email = "123@qq.com"
            };
            _userLogicMock.Setup(u => u.Create(_userLogicModel));
            _userLogicMock.Setup(u => u.CheckUserByName(It.IsAny<string>())).Returns(false).Verifiable();
            //Act
            var actual = _userController.CheckUserByName(userName);
            //Assert
            Assert.AreNotEqual(userName, actual);
        }

        [Test]
        public void GetById_should_return_model_that_has_same_property()
        {
            //Arrange
            int id = 10;
            _userLogicModel = new UserLogicModel()
            {
                UserId = "10",
                UserName = "lee",
                Password = "123",
                Email = "123@qq.com"
            };

            _userLogicMock.Setup(u => u.Get(It.IsAny<int>())).Returns(_userLogicModel).Verifiable();
            //Act
            var actual = _userController.GetById(id);
            //Assert
            Assert.AreEqual(_userLogicModel.UserName, actual.UserName);
            Assert.AreEqual(_userLogicModel.Password, actual.Password);
            Assert.AreEqual(_userLogicModel.Email, actual.Email);
        }
    }
}
