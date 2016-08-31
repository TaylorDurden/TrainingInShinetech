using System;
using System.Collections.Generic;
using BugManagement.Logic.ILogic;
using BugManagement.Logic.Models;
using BugManagemnet.WebAPI.Controllers;
using BugManagemnet.WebAPI.Models;
using Moq;
using NUnit.Framework;

namespace BugManagemnet.WebAPI.Test
{
    [TestFixture]
    public class UserControllerTest
    {
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
            _userLogicModel = null;
            _userLogicModels = null;
        }
        

        [Test]
        public void Login_should_call_login_method_of_user_logic_once_return_userViewModel()
        {
            //Arange
            var strEmail = "email";
            var strPassword = "password";
            _userLogicModel = new UserLogicModel()
            {
                UserId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastTime",
                Password = "test",
                LastLoginTime = DateTime.Now,
                RegisterTime = DateTime.Now,
                Status = "status",
                Type = "type"
            };
            _userLogicMock.Setup(n => n.LoginByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_userLogicModel);

            //Act
            var result = _userController.Login(strEmail, strPassword);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.UserId);
        }
        
        [Test]
        public void GetUserListViewModelByCondition_should_call_get_userListViewModel_by_condition_method_of_user_logic_once_return_userListViewModel_if_condition_is_not_exist()
        {
            //Arange
            var strCondition = "aa";
            var strPageIndex = "1";
            var count = 0;
            _userLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);

            //Act
            var result = _userController.GetUserListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            Assert.IsNull(result.Models);
            Assert.IsNull(result.Pages);

        }
        [Test]
        public void GetUserListViewModelByCondition_should_call_get_userListViewModel_by_condition_method_of_user_logic_once_return_userListViewModel_if_condition_is_exist()
        {
            //Arange
            var strCondition = "aa";
            var strPageIndex = "1";
            var count = 1;
            _userLogicModel = new UserLogicModel()
            {
                UserId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastTime",
                Password = "test",
                LastLoginTime = DateTime.Now,
                RegisterTime = DateTime.Now,
                Status = "status",
                Type = "type"
            };
            _userLogicModels = new List<UserLogicModel> { _userLogicModel };
            _userLogicMock.Setup(n => n.GetPageCountByCondition(It.IsAny<string>())).Returns(count);
            _userLogicMock.Setup(n => n.GetUserLogicModelsByCondition(It.IsAny<string>(),It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(_userLogicModels);

            //Act
            var result = _userController.GetUserListViewModelByCondition(strCondition, strPageIndex);

            //Assert
            Assert.IsNotNull(result.Models);
            Assert.IsNotNull(result.Pages);
            Assert.AreEqual(1, result.Models.Count);
        }
        
        [Test]
        public void CreateUserByRegister_should_call_create_user_by_register_method_of_user_logic_once()
        {
            //Arange
            var registerViewModel = new RegisterViewModel()
            {
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastTime",
                Password = "test"
            };

            //Act
            _userController.CreateUserByRegister(registerViewModel);

            //Assert
            _userLogicMock.Verify(n => n.Create(It.IsAny<UserLogicModel>()), Times.Once);
        }
        
        [Test]
        public void CreateUser_should_call_create_user_method_of_user_logic_once()
        {
            //Arange
            var userViewModel = new UserViewModel()
            {
                UserId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastTime",
                Password = "test",
                LastLoginTime = DateTime.Now,
                RegisterTime = DateTime.Now,
                Status = "status",
                Type = "type"
            };

            //Act
            _userController.CreateUser(userViewModel);

            //Assert
            _userLogicMock.Verify(n => n.Create(It.IsAny<UserLogicModel>()), Times.Once);
        }
        
        [Test]
        public void GetUserById_should_call_get_user_by_Id_method_of_user_logic_once_return_userViewModel()
        {
            //Arange
            var userId = 1;
            _userLogicModel = new UserLogicModel()
            {
                UserId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastTime",
                Password = "test",
                LastLoginTime = DateTime.Now,
                RegisterTime = DateTime.Now,
                Status = "status",
                Type = "type"
            };
            _userLogicMock.Setup(n => n.Get(It.IsAny<int>())).Returns(_userLogicModel);

            //Act
            var result = _userController.GetUserById(userId);

            //Assert
            _userLogicMock.Verify(n => n.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("fristName", result.FristName);
            Assert.AreEqual("test", result.Password);
        }
        
        [Test]
        public void DeleteUserById_should_call_delete_user_by_Id_method_of_user_logic_once_()
        {
            //Arange
            var userId = 1;

            //Act
            _userController.DeleteUserById(userId);

            //Assert
            _userLogicMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }
        
        [Test]
        public void UpdateUser_should_call_update_user_method_of_user_logic_once()
        {
            //Arange
            var userViewModel = new UserViewModel()
            {
                UserId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastTime",
                Password = "test",
                LastLoginTime = DateTime.Now,
                RegisterTime = DateTime.Now,
                Status = "status",
                Type = "type"
            };

            //Act
            _userController.UpdateUser(userViewModel);

            //Assert
            _userLogicMock.Verify(n => n.Edit(It.IsAny<UserLogicModel>()), Times.Once);
        }
        
        [Test]
        public void GetUsers_should_call_get_users_method_of_user_logic_once_return_userListViewModel()
        {
            _userLogicModel = new UserLogicModel()
            {
                UserId = 1,
                Email = "Email@qq.com",
                FristName = "fristName",
                LastName = "lastTime",
                Password = "test",
                LastLoginTime = DateTime.Now,
                RegisterTime = DateTime.Now,
                Status = "status",
                Type = "type"
            };
            _userLogicModels = new List<UserLogicModel>() { _userLogicModel };
            _userLogicMock.Setup(n => n.GetAll()).Returns(_userLogicModels);

            //Act
            var result = _userController.GetUsers();

            //Assert
            _userLogicMock.Verify(n => n.GetAll(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);

        }
        
        [Test]
        public void CheckUserEmail_should_call_check_userEmail_method_of_user_logic_once_return_false_if_userEmail_is_not_exist()
        {
            //Arange
            var strUserEmail = "";
            var strUserId = "";
            _userLogicMock.Setup(n => n.CheckExist(It.IsAny<string>(),It.IsAny<string>())).Returns(false);

            //Act
            var result = _userController.CheckUserEmail(strUserEmail, strUserId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckUserEmail_should_call_check_userEmail_method_of_user_logic_once_return_true_if_userEmail_is_exist()
        {
            //Arange
            var strUserEmail = "";
            var strUserId = "";
            _userLogicMock.Setup(n => n.CheckExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //Act
            var result = _userController.CheckUserEmail(strUserEmail, strUserId);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
