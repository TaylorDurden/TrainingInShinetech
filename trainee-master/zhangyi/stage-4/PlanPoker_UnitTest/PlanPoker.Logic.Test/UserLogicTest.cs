using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using PlanPoker.Data.Models;
using PlanPoker.ILogic.LogicModel;
using PlanPoker.IRepository;
using PlanPoker.Repository.UnitOfWork;

namespace PlanPoker.Logic.Test
{
    [TestFixture()]
    public class UserLogicTest
    {
        private string _username;
        private string _password;
        private string _email;
        private int _userId;
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IUnitOfWorkFactory> _userUnitOfWorkFactoryMock;
        private Mock<IUnitOfWork> _uniteOfWorkMock;
        private UserLogic _userLogic;
        private User _userEntity;
        private List<User> _userEntities;
        [SetUp]
        public void Init()
        {
            _userRepositoryMock=new Mock<IUserRepository>();
            _userUnitOfWorkFactoryMock=new Mock<IUnitOfWorkFactory>();
            _userLogic =new UserLogic(_userRepositoryMock.Object, _userUnitOfWorkFactoryMock.Object);
            _uniteOfWorkMock = new Mock<IUnitOfWork>();
        }

        [TearDown]
        public void Dispose()
        {
            _userRepositoryMock = null;
            _userUnitOfWorkFactoryMock = null;
            _userLogic = null;
            _uniteOfWorkMock = null;
        }

        [Test]
        public void GetAll_should_call_query_method_of_user_repository_once_return_all_users()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            _userEntities = new List<User> {_userEntity};
            _userRepositoryMock.Setup(x => x.Query()).Returns(_userEntities.AsQueryable());
            //Act
            var result = _userLogic.GetAll();
            //Assert
            _userRepositoryMock.Verify(x => x.Query(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void Delete_should_call_delete_method_of_user_repository_once()
        {
            //Arange
            _userId = 5;
            _userUnitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);

            //Act
            _userLogic.Delete(_userId);

            //Assert
            _userRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Get_should_call_get_method_of_user_repository_once_return_user()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            _userRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(_userEntity);

            //Act
            var result = _userLogic.Get(_userId);

            //Assert
            _userRepositoryMock.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual(_userId,result.UserId);
            Assert.AreEqual(_username, result.UserName);
            Assert.AreEqual(_password, result.Password);
            Assert.AreEqual(_email, result.Email);
        }

        [Test]
        public void Create_should_call_create_method_of_user_repository_once_return_userId()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            var userLogicModel = new UserLogicModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            _userUnitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);
            _userRepositoryMock.Setup(x => x.Create(_userEntity));

            //Act
            var result = _userLogic.Create(userLogicModel);

            //Assert
            _userRepositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
            Assert.Greater(result, 0);
        }

        [Test]
        public void Edit_should_call_edit_method_of_user_repository_once()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _email = "zhangyi@shinetechchina.com";
            _userId = 5;
            var userLogicModel = new UserLogicModel
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
            };
            _userEntity = new User 
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            _userUnitOfWorkFactoryMock.Setup(x => x.GetCurrentUnitOfWork()).Returns(_uniteOfWorkMock.Object);

            //Act
            _userLogic.Edit(userLogicModel);

            //Assert
            _userRepositoryMock.Verify(x => x.Edit(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Login_should_call_login_method_of_user_repository_once_return_userId()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _email = "zhangyi@shinetechchina.com";
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            _userRepositoryMock.Setup(x => x.Get(_username)).Returns(_userEntity);

            //Act
            var result = _userLogic.Login(_username, _password);

            //Assert
            _userRepositoryMock.Verify(x => x.Get(_username), Times.Once);
            Assert.AreEqual(_userId, result);
            Assert.Greater(result, 0);
        }

        [Test]
        public void Login_should_call_login_method_of_user_repository_once_return_zero_if_password_not_equal()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _email = "zhangyi@shinetechchina.com";
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            _userRepositoryMock.Setup(x => x.Get(_username)).Returns(_userEntity);

            //Act
            var result = _userLogic.Login(_username, It.IsAny<string>());

            //Assert
            _userRepositoryMock.Verify(x => x.Get(_username), Times.Once);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Login_should_call_login_method_of_user_repository_and_return_zero_when_user_not_exist()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _email = "zhangyi@shinetechchina.com";
            _userEntity = null;
            _userRepositoryMock.Setup(x => x.Get(_username)).Returns(_userEntity);

            //Act
            var result = _userLogic.Login(_username, _password);

            //Assert
            _userRepositoryMock.Verify(x => x.Get(_username), Times.Once);
            Assert.AreEqual(result, 0);
        }

        [Test]
        public void CheckIfExist_should_call_query_method_of_user_repository_and_return_true_when_user_is_exist()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _email = "zhangyi@shinetechchina.com";
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            _userEntities = new List<User> {_userEntity};
            _userRepositoryMock.Setup(x => x.Query()).Returns(_userEntities.AsQueryable());

            //Act
            var result = _userLogic.CheckIfExist(_username);

            //Assert
            _userRepositoryMock.Verify(x => x.Query(), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void QueryByName_should_call_query_method_of_user_repository_and_return_users()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _email = "zhangyi@shinetechchina.com";
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email
            };
            _userEntities = new List<User> {_userEntity};
            _userRepositoryMock.Setup(x => x.Query()).Returns(_userEntities.AsQueryable());

            //Act
            var result = _userLogic.QueryByName(_username);

            //Assert
            _userRepositoryMock.Verify(x => x.Query(), Times.AtLeastOnce);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(),1);
        }

        [Test]
        public void GetUserImage_should_call_get_method_of_user_repository_and_return_user_image()
        {
            //Arange
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _email = "zhangyi@shinetechchina.com";
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
                Image = "myImg"
            };
            _userRepositoryMock.Setup(x => x.Get(_username)).Returns(_userEntity);

            //Act
            var result = _userLogic.GetUserImage(_username);

            //Assert
            _userRepositoryMock.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetUsersByUserIds_should_call_get_method_of_user_repository_and_return_all_users_id_in_ids()
        {
            //Arange
            var ids = new List<int> {5, 6};
            _username = "JoyZhang";
            _password = "123456";
            _userId = 5;
            _email = "zhangyi@shinetechchina.com";
            _userEntity = new User
            {
                UserId = _userId,
                UserName = _username,
                Password = _password,
                Email = _email,
                Image = "myImg"
            };
            _userEntities = new List<User> { _userEntity };
            _userRepositoryMock.Setup(x => x.Get(_userId)).Returns(_userEntity);

            //Act
            var result = _userLogic.GetUsersByUserIds(ids);

            //Assert
            Assert.IsNotNull(result);
        }

    }
}