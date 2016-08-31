using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.ILogic;
using PlanPoker.ILogic.LogicModel;
using PlanPoker.WebAPI.Controllers;
using PlanPoker.WebAPI.Models;

namespace PlanPoker.WebApi.Test
{
    [TestFixture]
    public class UserControllerTest
    {
        private string _username;
        private string _password;
        private string _email;
        private int _userId;
        private Mock<IUserLogic> _userLogicMock;
        private UserController _userController;
        private UserLogicModel _userLogicModel;
        private List<UserLogicModel> _userLogicModels;

        [SetUp]
        public void Init()
        {
            _userLogicMock = new Mock<IUserLogic>();
            _userController = new UserController(_userLogicMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _userLogicMock = null;
            _userController = null;
        }

        [Test]
        public void GetAll_should_call_GetAll_method_of_user_logic_once_return_all_users()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userLogicModels = new List<UserLogicModel>();
            _userLogicModel = new UserLogicModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            _userLogicModels.Add(_userLogicModel);
            _userLogicMock.Setup(x => x.GetAll()).Returns(_userLogicModels);

            //Act
            var result = _userController.GetAll();

            //Assert
            _userLogicMock.Verify(x => x.GetAll(), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Any());
        }

        [Test]
        public void GetById_should_call_get_method_of_user_logic_once_return_user_searched_by_userId()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userLogicModel = new UserLogicModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            _userLogicMock.Setup(x => x.Get(It.IsAny<int>())).Returns(_userLogicModel);

            //Act
            var result = _userController.GetById(_userId);

            //Assert
            _userLogicMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(_userId, result.UserId);
            Assert.AreEqual(_username, result.UserName);
            Assert.AreEqual(_password, result.Password);
            Assert.AreEqual(_email, result.Email);
        }

        [Test]
        public void Delete_should_call_delete_method_of_user_logic_once()
        {
            //Arange
            _userId = 5;

            //Act
            _userController.Delete(_userId);

            //Assert
            _userLogicMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Create_should_call_create_method_of_user_logic_once_return_userId()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userLogicModel = new UserLogicModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            var userViewModel = new UserViewModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            _userLogicMock.Setup(x => x.Create(_userLogicModel)).Returns(_userId);

            //Act
            var result = _userController.Create(userViewModel);

            //Assert
            _userLogicMock.Verify(x => x.Create(It.IsAny<UserLogicModel>()), Times.Once);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void Edit_should_call_edit_method_of_user_logic_once()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userLogicModel = new UserLogicModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            var userViewModel = new UserViewModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };

            //Act
            _userController.Edit(userViewModel);

            //Assert
            _userLogicMock.Verify(x => x.Edit(It.IsAny<UserLogicModel>()), Times.Once);
        }

        #region Test UserLogin
        [Test]
        public void Login_should_login_method_of_user_logic_once_return_userId()
        {
            //Arange
            UserViewModel userViewModel = new UserViewModel
            {
                UserName = "JoyZhang",
                Password = "123456",
                UserId = 5
            };
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _userLogicMock.Setup(x => x.Login(_username, _password)).Returns(_userId);

            //Act
            var result = _userController.Login(userViewModel);

            //Assert
            _userLogicMock.Verify(x => x.Login(_username, _password), Times.Once);
            Assert.Greater(result, 0);
            Assert.AreEqual(_userId, result);
        }
        #endregion

        [Test]
        public void QueryByName_should_queryByName_method_of_user_logic_once_return_users()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userLogicModels = new List<UserLogicModel>();
            _userLogicModel = new UserLogicModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            _userLogicModels.Add(_userLogicModel);
            _userLogicMock.Setup(x => x.QueryByName(_username)).Returns(_userLogicModels);

            //Act
            var result = _userController.QueryByName(_username);

            //Assert
            _userLogicMock.Verify(x => x.QueryByName(_username), Times.Once);
            Assert.IsNotNull(_userLogicMock.Object.QueryByName(_username));
            Assert.AreEqual(1, result.Count());
        }
    }
}
