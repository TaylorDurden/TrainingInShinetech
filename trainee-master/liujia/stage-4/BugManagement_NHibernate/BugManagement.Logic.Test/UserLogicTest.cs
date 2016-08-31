using System;
using System.Collections.Generic;
using System.Linq;
using BugManagement.DAL;
using BugManagement.DAL.IRepository;
using BugManagement.DAL.Model;
using BugManagement.DAL.UnitOfWork;
using BugManagement.Logic.Logic;
using BugManagement.Logic.Models;
using Moq;
using NUnit.Framework;

namespace BugManagement.Logic.Test
{
    [TestFixture]
    public class UserLogicTest
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUnitOfWorkFactory> _unitOfWorkFactotyMock;
        private UserLogic _userLogic;
        private Mock<IUnitOfWork> _uniteOfWorkMock;

        [SetUp]
        public void Init()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _unitOfWorkFactotyMock = new Mock<IUnitOfWorkFactory>();
            _uniteOfWorkMock = new Mock<IUnitOfWork>();

            _userLogic = new UserLogic(_userRepositoryMock.Object, _unitOfWorkFactotyMock.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _userRepositoryMock = null;
            _unitOfWorkFactotyMock = null;
            _uniteOfWorkMock = null;
            _userLogic = null;
        }

        [Test]
        public void Create_should_call_create_method_of_user_repository_once()
        {
            //Arange
            var userLogicModel = new UserLogicModel()
            {
                UserId=1,
                Email="email",
                FristName="fristname",
                LastName="lastname",
                Password="password",
                Status="status",
                Type="type",
                RegisterTime=DateTime.Now,
                LastLoginTime=DateTime.Now
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _userRepositoryMock.Setup(n => n.Create(It.IsAny<User>()));

            //Act
            _userLogic.Create(userLogicModel);

            //Assert
            _userRepositoryMock.Verify(n => n.Create(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Edit_should_call_edit_method_of_user_repository_once()
        {
            //Arange
            var userLogicModel = new UserLogicModel()
            {
                UserId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _userRepositoryMock.Setup(n => n.Edit(It.IsAny<User>()));

            //Act
            _userLogic.Edit(userLogicModel);

            //Assert
            _userRepositoryMock.Verify(n => n.Edit(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Delete_should_call_delete_method_of_user_repository_once()
        {
            //Arange
            var userId = 1;
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _userRepositoryMock.Setup(n => n.Delete(It.IsAny<int>()));

            //Act
            _userLogic.Delete(userId);

            //Assert
            _userRepositoryMock.Verify(n => n.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_menthod_of_user_repository_once_return_userLogicModel()
        {
            //Arange
            var userId = 1;
            var user = new User()
            {
                UserId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };
            _userRepositoryMock.Setup(n => n.Get(It.IsAny<int>())).Returns(user);

            //Act
            var result = _userLogic.Get(userId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("email", result.Email);
        }

        [Test]
        public void GetAll_should_call_get_all_method_of_user_repository_once_return_userLogicModel_list()
        {
            //Arange
            var user = new User()
            {
                UserId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };
            var users = new List<User>() { user };
            _userRepositoryMock.Setup(n => n.Query()).Returns(users.AsQueryable());

            //Act
            var result = _userLogic.GetAll();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void LoginByEmailAndPassword_should_call_login_by_email_and_password_method_of_user_repository_once_return_null_when_email_or_password_is_not_exist()
        {
            //Arange
            var email = "";
            var password = "";

            //Act
            var result = _userLogic.LoginByEmailAndPassword(email, password);

            //Assert
            Assert.IsNull(result);
        }

        [Test]
        public void LoginByEmailAndPassword_should_call_login_by_email_and_password_method_of_user_repository_once_return_userLogicModel_when_email_and_password_is_exist()
        {
            //Arange
            var email = "email";
            var password = "password";
            var user = new User()
            {
                UserId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };
            var users = new List<User>() { user };
            _unitOfWorkFactotyMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _userRepositoryMock.Setup(n => n.Query()).Returns(users.AsQueryable());
            _userRepositoryMock.Setup(n => n.Get(It.IsAny<int>())).Returns(user);
            _userRepositoryMock.Setup(n => n.Edit(It.IsAny<User>()));

            //Act
            var result = _userLogic.LoginByEmailAndPassword(email, password);

            //Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetPageCountByCondition_should_call_get_pageCount_by_condition_method_of_project_repository_once_return_pageCount()
        {
            //Arange
            var strSerchCondition = "";
            var user = new User()
            {
                UserId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };
            var users = new List<User>() { user };
            _userRepositoryMock.Setup(n => n.Query()).Returns(users.AsQueryable());

            //Act
            var result = _userLogic.GetPageCountByCondition(strSerchCondition);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetProjectLogicModelsByCondition_should_call_get_projectLogicModels_by_condition_method_of_project_repository_once_return_projectLogicModel_list()
        {
            //Arange
            var strSerchCondition = "";
            var pageIndex = 5;
            var pageSize = 3;
            var count = 6;
            var user = new User()
            {
                UserId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };
            var users = new List<User>() { user, user, user, user, user, user, user };
            _userRepositoryMock.Setup(n => n.Query()).Returns(users.AsQueryable());

            //Act
            var result = _userLogic.GetUserLogicModelsByCondition(strSerchCondition, pageIndex, pageSize, count);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public void CheckExist_should_call_check_exist_method_of_user_repository_once_return_false_when_condition_is_not_exist()
        {
            //Arange
            var strProjectName = "";
            var strProjectId = "";
            var users = new List<User>();
            _userRepositoryMock.Setup(n => n.Query()).Returns(users.AsQueryable());

            //Acrt
            var result = _userLogic.CheckExist(strProjectName, strProjectId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void CheckExist_should_call_check_exist_method_of_user_repository_once_return_true_when_condition_is_exist()
        {
            //Arange
            var strEmail = "email";
            var strUserId = "2";
            var user = new User()
            {
                UserId = 1,
                Email = "email",
                FristName = "fristname",
                LastName = "lastname",
                Password = "password",
                Status = "status",
                Type = "type",
                RegisterTime = DateTime.Now,
                LastLoginTime = DateTime.Now
            };
            var users = new List<User>() { user };
            _userRepositoryMock.Setup(n => n.Query()).Returns(users.AsQueryable());

            //Acrt
            var result = _userLogic.CheckExist(strEmail, strUserId);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
